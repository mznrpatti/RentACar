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

            Response.Headers.Add("Access-Control-Allow-Origin", "*");

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

                Response.Headers.Add("Access-Control-Allow-Origin", "*");

                return Ok(allcars);
            }

            var cars = _carRepository.GetCars().Where(c => string.Equals(c.CategoryName, category, StringComparison.OrdinalIgnoreCase)).ToList();

            Response.Headers.Add("Access-Control-Allow-Origin", "*");

            return Ok(cars);
        }

        [HttpGet("{carId}/availability")]
        public IActionResult GetRentalAvailability(int carId)
        {
            var availability = _carRepository.GetRentalAvailability(carId);

            if (availability == null)
            {
                return NotFound();
            }

            Response.Headers.Add("Access-Control-Allow-Origin", "*");

            return Ok(availability);
        }

    }
}
