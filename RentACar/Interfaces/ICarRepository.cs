using RentACar.Models;

namespace RentACar.Interfaces
{
    public interface ICarRepository
    {
        IList<CarModel> GetCars();
        List<Rental> GetRentalAvailability(int carId);
        List<Rental> GetOverlappingRentals(DateTime fromDate, DateTime toDate, int carId);
       // void AddRental(RentACar.Models.Entities.Rental rental);
       // void AddRental(RentalModel rentalModel);
        CarModel GetCarById(int carId);
        bool CheckAvailability(int carId, DateTime fromDate, DateTime toDate);

    }
}
