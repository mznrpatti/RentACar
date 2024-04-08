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
    }
}
