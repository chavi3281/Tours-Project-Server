using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassToFlightController : ControllerBase
    {
        IBlClassToFlight classToFlight;

        public ClassToFlightController(Ibl bl)
        {
            this.classToFlight = bl.ClassToFlight;
        }

        // GET: GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await classToFlight.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: GetAllSales
        [HttpGet("GetAllSales")]
        public async Task<IActionResult> GetAllSales()
        {
            try
            {
                var result = await classToFlight.GetAllSales();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: GetByClassFlight
        [HttpGet("GetByClassFlight/{cl}/{flightId}")]
        public async Task<IActionResult> GetByClassFlight(string cl, int flightId)
        {
            try
            {
                if (string.IsNullOrEmpty(cl))
                {
                    return BadRequest("Class name cannot be null or empty");
                }

                var bl = await classToFlight.GetByClassFlightId(cl, flightId);
                

                return Ok(bl);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: Add
        [HttpPost("Add")]
        public async Task<IActionResult> Create(BlClassToFlight ctf)
        {
            try
            {
                if (ctf == null)
                {
                    return BadRequest("Class to flight data is null");
                }

                await classToFlight.Create(ctf);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT
        [HttpPut("Update")]
        public async Task<IActionResult> Put(BlClassToFlight ctf)
        {
            try
            {
                if (ctf == null)
                {
                    return BadRequest("Class to flight data is null");
                }

                var result = await classToFlight.Update(ctf);
                

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
                    return BadRequest("Invalid ID");
                }

                await classToFlight.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
