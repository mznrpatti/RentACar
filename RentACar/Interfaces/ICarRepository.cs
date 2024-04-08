using RentACar.Models;

namespace RentACar.Interfaces
{
    public interface ICarRepository
    {
        IList<CarModel> GetCars();
        CarModel GetCarById(int carId);

    }
}
