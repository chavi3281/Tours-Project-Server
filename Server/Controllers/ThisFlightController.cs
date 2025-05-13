using BL.Api;
using BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public List<BlThisFlight> Get()
        {
            return thisFlight.GetAll();
        }

        // GET: GetById
        [HttpGet("GetById/{src}/{des}/{date}")]
        public IActionResult GetBySrcDesDate(string src, string des, DateOnly date)
        {
            return Ok(thisFlight.GetBySrcDesDate(src, des, date));
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(thisFlight.GetById(id));
        }


        // POST: Add
        [HttpPost("Add")]
        public Task<BlThisFlight> Create(BlThisFlight thisFlights)
        {
           return thisFlight.Create(thisFlights);
           
        }

        // PUT
        [HttpPut("Update")]
        public List<BlThisFlight>? Update(BlThisFlight thisFlights)
        {
            return  thisFlight.Update(thisFlights);
        }

        // DELETE 
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            thisFlight.Delete(id);
        }
    }
}
