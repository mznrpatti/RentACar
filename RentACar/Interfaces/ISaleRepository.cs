using Microsoft.AspNetCore.Mvc;
using RentACar.Models;
using RentACar.Models.Entities;

namespace RentACar.Interfaces
{
    public interface ISaleRepository
    {
        public Task<IList<SaleModel>> GetAllSales();
        public Task<IList<SaleModel>> DeleteSale(int id);
        public bool CarExists(int id);
        public Task<IList<SaleModel>> CreateSale(CreateSaleModel createSaleModel);
    }
}
