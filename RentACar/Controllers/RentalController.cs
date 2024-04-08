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

        [HttpPost]
        public IActionResult RentCar(RentalDateModel rentalDateModel)
        {
            List<DateTime> availableDays = _rentalRepository.GetAvailableDates(rentalDateModel.CarId);
            string format = "yyyy-MM-dd";
            DateTime fromDate;
            DateTime.TryParseExact(rentalDateModel.FromDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate);
            DateTime toDate;
            DateTime.TryParseExact(rentalDateModel.ToDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out toDate);
            if (fromDate.CompareTo(toDate)>0)
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
                    RentalModel rentalModel = new RentalModel
                    {
                        CarId = rentalDateModel.CarId,
                        UserId = _rentalRepository.GetUserId(rentalDateModel.Username),
                        FromDate = fromDate,
                        ToDate = toDate,
                        Created = DateTime.Now
                    };

                    _rentalRepository.AddRental(rentalModel);

                    return Ok("Car successfully reserved!");
                }
            }
        }
    }
}
