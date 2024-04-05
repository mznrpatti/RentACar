using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Interfaces;
using RentACar.Models;
using RentACar.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RentACar.Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SaleRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IList<SaleModel> GetAllSales()
        {
            var allSales = _context.Sales.Include(s => s.Car).ToList();
            var saleModels = allSales.Select(s => new SaleModel
            {
                CarBrand = s.Car.Brand,
                CarModel = s.Car.Model,  
                Description = s.Description,
                Percentage = s.Percentage,
                ChangedPrice = s.Car.DailyPrice - (s.Car.DailyPrice * s.Percentage / 100) 
            }).ToList();
            return saleModels;
        }


    }
}
