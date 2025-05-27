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
    public class DestinitionController : ControllerBase
    {
        IBlDestination destination;

        public DestinitionController(Ibl bl)
        {
            this.destination = bl.Destination;
        }

        // GET: GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await destination.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: GetById
        [HttpGet("GetById/{dest}")]
        public async Task<IActionResult> GetById(string dest)
        {
            try
            {
                if (string.IsNullOrEmpty(dest))
                {
                    return BadRequest("Destination cannot be null or empty");
                }

                var result = await destination.GetById(dest);
                if (result == null)
                {
                    return NotFound($"Destination '{dest}' not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: Add
        [HttpPost("Add")]
        public async Task<IActionResult> Create(BlDestination des)
        {
            try
            {
                if (des == null)
                {
                    return BadRequest("Destination data is null");
                }

                var result = await destination.Create(des);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: UPDATE
        [HttpPut("update")]
        public async Task<IActionResult> Update(BlDestination des)
        {
            try
            {
                if (des == null)
                {
                    return BadRequest("Destination data is null");
                }

                var result = await destination.Update(des);
                if (result == null)
                {
                    return NotFound("Destination not found or update failed");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE 
        [HttpDelete("{des}")]
        public async Task<IActionResult> Delete(int des)
        {
            try
            {
                if (des <= 0)
                {
                    return BadRequest("Invalid destination ID");
                }

                await destination.Delete(des);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
