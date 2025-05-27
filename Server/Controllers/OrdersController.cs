using BL.Api;
using BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Orders : ControllerBase
    {
        IBlOrder Order;

        public Orders(Ibl bl)
        {
            Order = bl.Order;
        }

        // GET: api/<CustomersController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await Order.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET:
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid order ID");
                }

                var order = await Order.GetById(id);
                if (order == null)
                {
                    return NotFound($"Order with ID {id} not found");
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET:
        [HttpGet("GetByCustomerId/{id}")]
        public async Task<IActionResult> GetByCustomerId(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid customer ID");
                }

                var orders = await Order.GetByCustomerId(id);
                if (orders == null || orders.Count == 0)
                {
                    return NotFound($"No orders found for customer with ID {id}");
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: GetByClassToFlightId
        [HttpGet("GetByClassToFlightId/{id}")]
        public async Task<IActionResult> GetByClassToFlightId(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid class to flight ID");
                }

                var orders = await Order.GetByClassToFlightId(id);
                if (orders == null || orders.Count == 0)
                {
                    return NotFound($"No orders found for class to flight with ID {id}");
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST api/<CustomersController>
        [HttpPost("Add")]
        public async Task<IActionResult> Create(BlOrder o)
        {
            try
            {
                if (o == null)
                {
                    return BadRequest("Order data is null");
                }

                await Order.Create(o);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid order ID");
                }

                await Order.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
