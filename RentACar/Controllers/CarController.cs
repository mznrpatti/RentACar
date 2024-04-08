using Microsoft.AspNetCore.Mvc;
using RentACar.Interfaces;
using RentACar.Models;

namespace RentACar.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet]
        public IActionResult GetCars()
        {
            var cars = _carRepository.GetCars();

            return Ok(cars);
        }

        [HttpGet]
        public IActionResult GetCarsByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return BadRequest("Category cannot be null or empty");
            }
            else if(category.Equals("All"))
            {
                var allcars = _carRepository.GetCars();

                return Ok(allcars);
            }

            var cars = _carRepository.GetCars().Where(c => string.Equals(c.CategoryName, category, StringComparison.OrdinalIgnoreCase)).ToList();

            return Ok(cars);
        }

       /* [HttpPost]
        public IActionResult RentCar(RentalModel rentalModel)
        {
            var car = _carRepository.GetCarById(rentalModel.Id);

            if (car == null)
            {
                return NotFound("Car not found");
            }

            var available = IsCarAvailableForRental(rentalModel.FromDate, rentalModel.ToDate, car.Id);

            if (!available)
            {
                var nextAvailableDate = CalculateNextAvailableDate(rentalModel.FromDate, car.Id);
                return BadRequest($"Sorry, the car is already booked. It will be available from {nextAvailableDate.ToShortDateString()}");
            }

            var rental = new Rental
            {
                Id = car.Id,
                FromDate = rentalModel.FromDate,
                ToDate = rentalModel.ToDate
            };

            _carRepository.AddRental(rental);

            return Ok("Car rented successfully");
        }
       */
        /*private bool IsCarAvailableForRental(DateTime fromDate, DateTime toDate, int carId)
        {
            var overlappingRentals = _carRepository.GetOverlappingRentals(fromDate, toDate, carId);
            return overlappingRentals != null && overlappingRentals.Count == 0;
        }*/

        /*private DateTime CalculateNextAvailableDate(DateTime fromDate, int carId)
        {
            var nextAvailableDate = fromDate;

            while (!IsCarAvailableForRental(nextAvailableDate, nextAvailableDate.AddDays(30), carId))
            {
                nextAvailableDate = nextAvailableDate.AddDays(1);
            }

            return nextAvailableDate;
        }*/

        /*[HttpPost("{carId}/reserve")]
        public IActionResult ReserveCar(int carId, RentalModel rental)
        {
            if (rental.FromDate == null || rental.ToDate == null || rental.FromDate > rental.ToDate)
            {
                return BadRequest("Invalid rental dates. Please provide a valid start and end date for the rental.");
            }

            if (rental.ToDate.Subtract(rental.FromDate).TotalDays > 30)
            {
                return BadRequest("Maximum rental duration is 30 days.");
            }

            var isAvailable = _carRepository.CheckAvailability(carId, rental.FromDate, rental.ToDate);
            if (!isAvailable)
            {
                return BadRequest("Sorry, car is already booked during this period.");
            }

            // Implement logic to save the rental details (carId, rental dates, etc.) to the database
            // This might involve creating a new Rental object and using the ICarRepository to save it
            //....??????


            return Ok("Car successfully reserved!");
        }*/
    }
}
