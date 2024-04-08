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
    }
}
