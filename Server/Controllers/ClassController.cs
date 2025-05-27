using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        IBlClass classs;

        public ClassController(Ibl bl)
        {
            this.classs = bl.Classes;
        }

        // GET: GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await classs.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: GetById
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetByClassFlight(int id)
        {
            try
            {
                var result = await classs.GetById(id);
                if (result == null)
                {
                    return NotFound($"Class with ID {id} not found");
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
        public async Task<IActionResult> Create(BlClass ctf)
        {
            try
            {
                if (ctf == null)
                {
                    return BadRequest("Class data is null");
                }

                var result = await classs.Create(ctf);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT
        [HttpPut("Update")]
        public async Task<IActionResult> Put(BlClass ctf)
        {
            try
            {
                if (ctf == null)
                {
                    return BadRequest("Class data is null");
                }

                var result = await classs.Update(ctf);
                if (result == null)
                {
                    return NotFound($"Class not found or update failed");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE 
        [HttpDelete("Delete/{description}")]
        public async Task<IActionResult> Delete(string description)
        {
            try
            {
                if (string.IsNullOrEmpty(description))
                {
                    return BadRequest("Description cannot be null or empty");
                }

                var result = await classs.Delete(description);
                if (result == null)
                {
                    return NotFound($"Class with description '{description}' not found");
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
