using Microsoft.AspNetCore.Mvc;
using RentACar.Interfaces;
using RentACar.Repository;

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
            DateTime from = DateTime.Today;
            DateTime to = from.AddDays(30);

            List<DateTime> days = new List<DateTime>();
            DateTime currentDate = DateTime.Today;
            while (currentDate <= to)
            {
                days.Add(currentDate);
                currentDate = currentDate.AddDays(1);
            }

            List<DateTime> wrongDays=_rentalRepository.GetNotAvailableDates(carId);
            if(wrongDays.Count > 0)
            {
                List<DateTime> deleteDays = new List<DateTime>();
                foreach (var wrongDay in wrongDays)
                {
                    foreach (var day in days)
                    {
                        if (wrongDay.Year == day.Year && wrongDay.Month == day.Month && wrongDay.Day == day.Day)
                        {
                            deleteDays.Add(day);
                        }
                    }
                }
                foreach (var day in deleteDays)
                {
                    days.Remove(day);
                }
            }
            
            return Ok(days);
        }
    }
}
