using RentACar.Interfaces;
using RentACar.Models;

namespace RentACar.Repository
{
    public class CarRepository : ICarRepository
    {
        private List<Car> cars;

        public CarRepository()
        {
            cars = new List<Car>();
            cars.Add(new Car
            {
                Id = 1,
                Category = new Category {Id=1, Name="sedan"},
                Brand = "Opel",
                Model = "G Astra",
                DailyPrice = 10000
            });

            cars.Add(new Car
            {
                Id = 2,
                Category = new Category { Id = 2, Name = "SUV" },
                Brand = "Toyota",
                Model = "C-HR",
                DailyPrice = 15000
            });

            cars.Add(new Car
            {
                Id = 3,
                Category = new Category { Id = 2, Name = "sedan" },
                Brand = "Volkswagen",
                Model = "Passat",
                DailyPrice = 16000
            });
        }

        public IEnumerable<Car> GetCars()
        {
            return cars;
        }
    }
}
