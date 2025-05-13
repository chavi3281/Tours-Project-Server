using BL.Api;
using BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Get()
        {
            return Ok(destination.GetAll());
        }

        // GET: GetById
        [HttpGet("GetById/{dest}")]
        public IActionResult GetById(string dest)
        {
            return Ok(destination.GetById(dest));
        }


        // POST: Add
        [HttpPost("Add")]
        public IActionResult Create(BlDestination des)
        {
            return Ok(destination.Create(des));
        }

        // PUT: UPDATE
        [HttpPut("update")]
        public IActionResult Update(BlDestination des)
        {
           return  Ok(destination.Update(des));
        }


        // DELETE 
        [HttpDelete("{des}")]
        public void Delete(int des)
        {
            destination.Delete(des);
        }
    }
}
