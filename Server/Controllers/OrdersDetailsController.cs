using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersDetailsController : ControllerBase
    {
        IBlOrdersDetail ordersDetails;

        public OrdersDetailsController(Ibl bl)
        {
            this.ordersDetails = bl.OrderDetails;
        }

        // GET: GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await ordersDetails.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: GetById
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid order details ID");
                }

                var orderDetail = await ordersDetails.GetById(id);
                if (orderDetail == null)
                {
                    return NotFound($"Order details with ID {id} not found");
                }

                return Ok(orderDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: Add
        [HttpPost("Add")]
        public async Task<IActionResult> Create(List<BlOrdersDetail> od)
        {
            try
            {
                if (od == null || od.Count == 0)
                {
                    return BadRequest("Order details data is null or empty");
                }

                await ordersDetails.Create(od);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE 
        [HttpDelete("{idThisFlight}")]
        public async Task<IActionResult> Delete(int idThisFlight)
        {
            try
            {
                if (idThisFlight <= 0)
                {
                    return BadRequest("Invalid flight ID");
                }

                var result = await ordersDetails.Delete(idThisFlight);
                if (result == null)
                {
                    return NotFound($"No order details found for flight with ID {idThisFlight}");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
