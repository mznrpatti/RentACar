using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentACar.Interfaces;
using RentACar.Models;
using RentACar.Models.Entities;
using System.Data;

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

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult GetAllSales()
        {
            var allSales = _saleRepository.GetAllSales();

            Response.Headers.Add("Access-Control-Allow-Origin", "*");

            return Ok(allSales);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var sales = await _saleRepository.DeleteSale(id);
            return Ok(sales);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSale(CreateSaleModel createSaleModel)
        {
            if (_saleRepository.CarExists(createSaleModel.CarId))
            {
                var sales = await _saleRepository.CreateSale(createSaleModel);
                return Ok(sales);
            }
            else
            {
                return BadRequest("Car not foundd!");
            }
        }
    }
}
