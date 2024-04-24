using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using RentACar.Interfaces;
using RentACar.Models;
using RentACar.Repository;
using System.Globalization;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;

namespace RentACar.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalController(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        [HttpGet("{carId}")]
        public IActionResult GetRentalAvailability(int carId)
        {
            List<DateTime> days = _rentalRepository.GetAvailableDates(carId);
            var formattedDays = days.Select(d => d.Date.ToString("yyyy-MM-dd")).ToList();
            return Ok(formattedDays);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult RentCar(RentalDateModel rentalDateModel)
        {
            try
            {
                List<DateTime> availableDays = _rentalRepository.GetAvailableDates(rentalDateModel.CarId);
                string format = "yyyy-MM-dd";
                DateTime fromDate;
                DateTime.TryParseExact(rentalDateModel.FromDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate);
                DateTime toDate;
                DateTime.TryParseExact(rentalDateModel.ToDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out toDate);
                if (fromDate.CompareTo(toDate) > 0)
                {
                    return BadRequest("From date cannot be earlier than to date!");
                }
                else
                {
                    if (_rentalRepository.IsOverlap(rentalDateModel))
                    {
                        return BadRequest("Overlapping rentals! You can't reserve the car on these days!");
                    }
                    else
                    {
                        RentalModel rentalModel = _rentalRepository.RentalDateToRental(rentalDateModel);

                        _rentalRepository.AddRental(rentalModel);

                        return Ok("Car successfully reserved!");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Rental failed. " + ex.Message);
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult CalculatePrice(RentalDateModel rentalDateModel)
        {
            try
            {
                RentalModel rentalModel = _rentalRepository.RentalDateToRental(rentalDateModel);
                if (rentalModel.FromDate.CompareTo(rentalModel.ToDate) > 0)
                {
                    return BadRequest("From date cannot be earlier than to date!");
                }
                else
                {
                    if (_rentalRepository.IsOverlap(rentalDateModel))
                    {
                        return BadRequest("Overlapping rentals! You can't reserve the car on these days!");
                    }
                    else
                    {
                        int price = _rentalRepository.GetWholePrice(rentalModel);
                        return Ok("Expected price: " + price);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Price calculation failed. " + ex.Message);
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult GetUserRentals(string username)
        {
            try
            {
                var userId = _rentalRepository.GetUserId(username);
                var userRentals = _rentalRepository.GetUserRentals(userId);

                return Ok(userRentals);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve user rentals. " + ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllRentals()
        {
            try
            {
                var rentals = _rentalRepository.GetAllRentals();

                return Ok(rentals);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to retrieve rentals. " + ex.Message);
            }
        }

    }
}
