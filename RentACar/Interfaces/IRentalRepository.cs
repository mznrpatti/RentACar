using RentACar.Models;

namespace RentACar.Interfaces
{
    public interface IRentalRepository
    {
        public IList<RentalModel> GetAllRentals();
        public List<DateTime> GetNotAvailableDates(int carId);
        public List<DateTime> GetAvailableDates(int carId);
        public bool IsOverlap(RentalDateModel rentalDateModel);
        public int GetUserId(string username);
        public void AddRental(RentalModel rentalModel);
    }
}
