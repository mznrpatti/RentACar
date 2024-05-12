 using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RentACar.Interfaces;
using RentACar.Models;
using RentACar.Models.Entities;
using RentACar.WebSocket;
using RentACar.WebSocket.Handlers;
using System.Data;
using System.Runtime.InteropServices;

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

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var sales = await _saleRepository.DeleteSale(id);
            return Ok("Sale deleted successfully!");
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateSale(CreateSaleModel createSaleModel)
        {
            try
            {
                if (_saleRepository.CarExists(createSaleModel.CarId))
                {
                    var sales = await _saleRepository.CreateSale(createSaleModel);
                    return Ok("Sale created successfully!");
                    

                }
                else
                {
                    return BadRequest("Car not found!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Sale creation failed. " + ex.Message);
            }
        }
    }
}
