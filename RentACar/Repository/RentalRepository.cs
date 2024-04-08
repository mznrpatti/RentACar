using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Interfaces;
using RentACar.Models;
using RentACar.Models.Entities;
using System.Globalization;

namespace RentACar.Repository
{
    public class RentalRepository : IRentalRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RentalRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IList<RentalModel> GetAllRentals()
        {
            var allRentals = _context.Rentals.Include(r => r.Car).Include(r => r.User).ToList();
            var rentalModels = allRentals.Select(r => new RentalModel
            {
                CarId = r.Car.Id,
                UserId = r.User.Id,
                FromDate = r.FromDate,
                ToDate = r.ToDate,
                Created = r.Created
            }).ToList();

            return rentalModels;
        }

        public List<DateTime> GetNotAvailableDates(int carId)
        {
            List<DateTime> wrongDays = new List<DateTime>();

            foreach (var r in GetAllRentals())
            {
                if(r.CarId == carId)
                {
                    DateTime date = r.FromDate;
                    while (date <= r.ToDate)
                    {
                        wrongDays.Add(date);
                        date = date.AddDays(1);
                    }
                }
            }

            return wrongDays;
        }

        public List<DateTime> GetAvailableDates(int carId)
        {
            DateTime from = DateTime.Today;
            DateTime to = from.AddDays(30);

            List<DateTime> days = new List<DateTime>();
            DateTime currentDate = DateTime.Today;
            while (currentDate <= to)
            {
                days.Add(currentDate);
                currentDate = currentDate.AddDays(1);
            }

            List<DateTime> wrongDays = GetNotAvailableDates(carId);
            if (wrongDays.Count > 0)
            {
                List<DateTime> deleteDays = new List<DateTime>();
                foreach (var wrongDay in wrongDays)
                {
                    foreach (var day in days)
                    {
                        if (wrongDay.Year == day.Year && wrongDay.Month == day.Month && wrongDay.Day == day.Day)
                        {
                            deleteDays.Add(day);
                        }
                    }
                }
                foreach (var day in deleteDays)
                {
                    days.Remove(day);
                }
            }

            return days;
        }

        public bool IsOverlap(RentalDateModel rentalDateModel)
        {
            bool isOverlap = false;
            List<DateTime> availableDays = GetAvailableDates(rentalDateModel.CarId);
            string format = "yyyy-MM-dd";
            DateTime fromDate;
            DateTime.TryParseExact(rentalDateModel.FromDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate);
            DateTime toDate;
            DateTime.TryParseExact(rentalDateModel.ToDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out toDate);
            int indexOfFromDate = -1;
            int indexOfToDate = -1;
            for (int i = 0; i < availableDays.Count; i++)
            {
                if (availableDays[i].Date == fromDate.Date)
                {
                    indexOfFromDate = i;
                }
                if (availableDays[i].Date==toDate.Date)
                {
                    indexOfToDate = i;
                }
            }
            DateTime previousDate = fromDate;
            for (int i = indexOfFromDate + 1; i <= indexOfToDate; i++)
            {
                if (!(availableDays[i].Date == previousDate.AddDays(1).Date))
                {
                    isOverlap = true;
                    break;
                }
                previousDate=previousDate.AddDays(1);
            }

            return isOverlap;
        }

        public int GetUserId(string username)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());
            if (user != null)
            {
                return user.Id;
            }
            else
            {
                throw new Exception("User not found!");
            }
        }

        public void AddRental(RentalModel rentalModel)
        {
            var rental = _mapper.Map<Rental>(rentalModel);
            _context.Rentals.Add(rental);
            _context.SaveChanges();
        }
    }
}
