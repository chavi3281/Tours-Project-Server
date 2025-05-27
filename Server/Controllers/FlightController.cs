using BL.Api;
using BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        IBlFlight flights;

        public FlightController(Ibl bl)
        {
            flights = bl.Flights;
        }

        // GET: api/<CustomersController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await flights.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/<CustomersController>
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid flight ID");
                }

                var flight = await flights.GetById(id);
                if (flight == null)
                {
                    return NotFound($"Flight with ID {id} not found");
                }

                return Ok(flight);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST api/<CustomersController>
        [HttpPost("Add")]
        public async Task<IActionResult> Create(BlFlights flight)
        {
            try
            {
                if (flight == null)
                {
                    return BadRequest("Flight data is null");
                }

                var result = await flights.Create(flight);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT api/<CustomersController>/5
        [HttpPut("Update")]
        public async Task<IActionResult> Update(BlFlights flight)
        {
            try
            {
                if (flight == null)
                {
                    return BadRequest("Flight data is null");
                }

                var result = await flights.Update(flight);
                if (result == null)
                {
                    return NotFound("Flight not found or update failed");
                }

                return Ok(result);
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
                    return BadRequest("Invalid flight ID");
                }

                await flights.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
