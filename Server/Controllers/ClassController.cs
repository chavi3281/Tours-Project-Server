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
    public class ClassController : ControllerBase
    {

        IBlClass classs;

        public ClassController(Ibl bl)
        {
            this.classs = bl.Classes;
        }

        // GET: GetAll
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(classs.GetAll());
        }


        // GET: GetById
        [HttpGet("GetById/{id}")]
        public IActionResult GetByClassFlight(int id)
        {
            return Ok(classs.GetById(id));
        }


        // POST: Add
        [HttpPost("Add")]
        public IActionResult Create(BlClass ctf)
        {
            return Ok(classs.Create(ctf));
        }

        // PUT
        [HttpPut("Update")]
        public IActionResult Put(BlClass ctf)
        {
            return Ok(classs.Update(ctf));
        }

        // DELETE 
        [HttpDelete("Delete/{description}")]
        public IActionResult Delete(string description)
        {
            return Ok(classs.Delete(description));
        }
    }
}
