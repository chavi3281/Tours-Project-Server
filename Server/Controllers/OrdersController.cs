using BL.Api;
using BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class  Orders : ControllerBase
    {
        IBlOrder Order;

        public Orders(Ibl bl)
        {
            Order = bl.Order;
        }

        // GET: api/<CustomersController>
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(Order.GetAll());
        }

        // GET:
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(Order.GetById(id));
        }

        // GET:
        [HttpGet("GetByCustomerId/{id}")]
        public IActionResult GetByCustomerId(int id)
        {
            return Ok(Order.GetByCustomerId(id));
        }


        // GET: GetByClassToFlightId
        [HttpGet("GetByClassToFlightId/{id}")]
        public IActionResult GetByClassToFlightId(int id)
        {
            return Ok(Order.GetByClassToFlightId(id));
        }


        // POST api/<CustomersController>
        [HttpPost("Add")]
        public async void Create(BlOrder o)
        {
             Order.Create(o);
        }



        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Order.Delete(id);
        }
    }
}
