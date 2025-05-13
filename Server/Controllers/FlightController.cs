using BL.Api;
using BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public  Task<List<BlFlights>> Get()
        {
            return flights.GetAll();
        }

        // GET: api/<CustomersController>
        [HttpGet("GetById/{id}")]
        public BlFlights GetById(int id)
        {
            return flights.GetById(id);
        }


        // POST api/<CustomersController>
        [HttpPost("Add")]
        public async Task< List<BlFlights>> Create(BlFlights flight)
        {   
            return await flights.Create(flight);
        }

        // PUT api/<CustomersController>/5
        [HttpPut("Update")]
        public List<BlFlights> Update(BlFlights flight)
        {
            return flights.Update(flight);
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            flights.Delete(id);
        }
    }
}
