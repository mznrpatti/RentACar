﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Interfaces;
using RentACar.Models;

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
    }
}
