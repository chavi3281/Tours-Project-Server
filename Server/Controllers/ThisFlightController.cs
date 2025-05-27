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
    public class ThisFlightController : ControllerBase
    {
        IBlThisFlight thisFlight;

        public ThisFlightController(Ibl bl)
        {
            this.thisFlight = bl.ThisFlight;
        }

        // GET: GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await thisFlight.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: GetById
        [HttpGet("GetById/{src}/{des}/{date}")]
        public async Task<IActionResult> GetBySrcDesDate(string src, string des, DateOnly date)
        {
            try
            {
                if (string.IsNullOrEmpty(src) || string.IsNullOrEmpty(des))
                {
                    return BadRequest("Source and destination cannot be null or empty");
                }

                var flights = await thisFlight.GetBySrcDesDate(src, des, date);
                if (flights == null || flights.Count == 0)
                {
                    return NotFound($"No flights found from '{src}' to '{des}' on {date}");
                }

                return Ok(flights);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid flight ID");
                }

                var flight = await thisFlight.GetById(id);
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

        // POST: Add
        [HttpPost("Add")]
        public async Task<IActionResult> Create(BlThisFlight thisFlights)
        {
            try
            {
                if (thisFlights == null)
                {
                    return BadRequest("Flight data is null");
                }

                var result = await thisFlight.Create(thisFlights);
                if (result == null)
                {
                    return StatusCode(500, "Failed to create flight");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT
        [HttpPut("Update")]
        public async Task<IActionResult> Update(BlThisFlight thisFlights)
        {
            try
            {
                if (thisFlights == null)
                {
                    return BadRequest("Flight data is null");
                }

                var result = await thisFlight.Update(thisFlights);
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

        // DELETE 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid flight ID");
                }

                var result = await thisFlight.Delete(id);
                if (result == null)
                {
                    return NotFound($"Flight with ID {id} not found or delete failed");
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
