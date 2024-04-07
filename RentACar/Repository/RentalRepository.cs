using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Interfaces;
using RentACar.Models;

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
    }
}
