﻿using Microsoft.AspNetCore.Mvc;
using RentACar.Interfaces;
using RentACar.Models;

namespace RentACar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type=typeof(IEnumerable<Car>))]
        public IActionResult GetCars()
        {
             var cars=_carRepository.GetCars();

            if (ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(cars);
        }
    }
}
