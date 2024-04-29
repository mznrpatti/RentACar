using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Interfaces;
using RentACar.Models;
using RentACar.Models.Entities;
using System;
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

        public async Task<IList<SaleModel>> GetAllSales()
        {
            var allSales = await _context.Sales.Include(s => s.Car).ToListAsync();
            var saleModels = allSales.Select(s => new SaleModel
            {
                CarId = s.Car.Id,
                CarBrand = s.Car.Brand,
                CarModel = s.Car.Model,
                Description = s.Description,
                Percentage = s.Percentage,
                ChangedPrice = s.Car.DailyPrice - (s.Car.DailyPrice * s.Percentage / 100)
            }).ToList();
            return saleModels;
        }

        public async Task<IList<SaleModel>> DeleteSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id) ?? throw new Exception("Sale not found!");
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return await GetAllSales();
        }

        public bool CarExists(int id)
        {
            var car=_context.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return false;
            return true;
        }

        public async Task<IList<SaleModel>> CreateSale(CreateSaleModel createSaleModel)
        {
            var sale = _mapper.Map<Sale>(createSaleModel);
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return await GetAllSales();
        }

    }
}
