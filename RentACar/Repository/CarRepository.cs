using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Interfaces;
using RentACar.Models;
using System.Collections.Generic;
using System.Linq;


namespace RentACar.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CarRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IList<CarModel> GetCars()
        {
            //get Cars as Queryable and put Category
            //mapping
            var carEntity = _context.Cars.AsQueryable().Include(c => c.Category).ToList();
            var carModel = _mapper.Map<IList<CarModel>>(carEntity);
            return carModel;
        }

        public List<Rental> GetRentalAvailability(int carId)
        {
            var rentalAvailabilities = _context.Rentals
                                       .Where(r => r.CarId == carId)
                                       .Select(r => new Rental
                                       {
                                           Id = r.Id,
                                           FromDate = r.FromDate,
                                           ToDate = r.ToDate
                                       })
                                       .ToList();

            return rentalAvailabilities;
        }
        

        public List<Rental> GetOverlappingRentals(DateTime fromDate, DateTime toDate, int carId)
        {
            List<RentACar.Models.Rental> result = new List<RentACar.Models.Rental>();

            var overlappingEntities = _context.Rentals
                               .Where(r => r.Id == carId && !(r.FromDate >= toDate || r.ToDate <= fromDate))
                               .ToList();

            foreach (var entity in overlappingEntities)
            {

                var modelRental = new RentACar.Models.Rental
                {
                    Id = entity.Id,
                };
                result.Add(modelRental);
            }

            return result;
        }

       /* public void AddRental(RentACar.Models.Entities.Rental rental)
        {

            var newRental = new Rental
            {
                Id = rental.Id,
                FromDate = rental.FromDate,
                ToDate = rental.ToDate
            };

            _context.Rentals.Add(rental);
            _context.SaveChanges();
        }*/

        /* public void AddRental(RentalModel rentalModel)
         {
             var rental = new Rental
             {
                 Id = rentalModel.Id,
                 FromDate = rentalModel.FromDate,
                 ToDate = rentalModel.ToDate
             };

             _context.Rentals.Add(rental);
             _context.SaveChanges();
         }*/

        public void AddRental(RentalModel rentalModel)
        {
            var rental = _mapper.Map<RentACar.Models.Entities.Rental>(rentalModel);
            _context.Rentals.Add(rental);
            _context.SaveChanges();
        }

        public CarModel GetCarById(int carId)
        {
            var carEntity = _context.Cars.FirstOrDefault(c => c.Id == carId);

            if (carEntity == null)
            {
                 return null;
                // throw new Exception("Car not found");
            }

            var carModel = _mapper.Map<CarModel>(carEntity);

            return carModel;
        }

        public bool CheckAvailability(int carId, DateTime fromDate, DateTime toDate)
        {
            
            var overlappingRentals = _context.Rentals
                .Any(r => r.CarId == carId && fromDate < r.ToDate && toDate > r.FromDate);

            return !overlappingRentals;
        }

    }
}
