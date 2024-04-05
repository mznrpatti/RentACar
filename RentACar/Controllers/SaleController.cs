using Microsoft.AspNetCore.Mvc;
using RentACar.Interfaces;

namespace RentACar.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleRepository _saleRepository;

        public SaleController(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        [HttpGet]
        public IActionResult GetAllSales()
        {
            var allSales = _saleRepository.GetAllSales();

            Response.Headers.Add("Access-Control-Allow-Origin", "*");

            return Ok(allSales);
        }

    }
}
