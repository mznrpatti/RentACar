using RentACar.Models;
using RentACar.Models.Entities;

namespace RentACar.Interfaces
{
    public interface ISaleRepository
    {
        public IList<SaleModel> GetAllSales();
    }
}
