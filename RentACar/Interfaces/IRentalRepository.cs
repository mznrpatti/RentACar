using RentACar.Models;

namespace RentACar.Interfaces
{
    public interface IRentalRepository
    {
        public IList<RentalModel> GetAllRentals();
        public List<DateTime> GetNotAvailableDates(int carId);
    }
}
