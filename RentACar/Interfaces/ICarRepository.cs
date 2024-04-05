using RentACar.Models;

namespace RentACar.Interfaces
{
    public interface ICarRepository
    {
        IList<CarModel> GetCars();
        List<Rental> GetRentalAvailability(int carId);
    }
}
