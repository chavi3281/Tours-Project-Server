using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Get()
        {
            
            return Ok(classToFlight.GetAll());
        }

        // GET: GetAll
        [HttpGet("GetAllSales")]
        public IActionResult GetAllSales()
        {
            return Ok(classToFlight.GetAllSales());
        }

        // GET: GetByClassFlight
        [HttpGet("GetByClassFlight/{cl}/{flightId}")]
        public IActionResult GetByClassFlight(string cl, int flightId)
        {
            BlClassToFlight bl = classToFlight.GetByClassFlightId(cl, flightId);
            return Ok(bl);
        }


        // POST: Add
        [HttpPost("Add")]
        public void Create(BlClassToFlight ctf)
        {
            classToFlight.Create(ctf);
        }

        // PUT
        [HttpPut("Update")]
        public IActionResult Put(BlClassToFlight ctf)
        {
            return Ok(classToFlight.Update(ctf));
        }

        // DELETE 
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            classToFlight.Delete(id);
        }
    }
}
