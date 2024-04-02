using RentACar.Models;

namespace RentACar.Interfaces
{
    public interface ICarRepository
    {
        public IEnumerable<Car> GetCars();
    }
}
