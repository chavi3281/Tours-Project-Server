using BL.Api;
using BL.Models;
using Dal.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    //הקריאות
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        IBlCustomers customers;

        public CustomersController(Ibl bl)
        {
            this.customers = bl.Customers;
        }

        // GET: GetAll
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(customers.GetAll());
        }

        // GET: GetById
        [HttpGet("GetById/{firstName}/{lastName}/{password}")]
        public IActionResult GetById(string firstName, string lastName, string password)
        {
            return Ok(customers.GetById(firstName, lastName, password));
        }


        // POST: Add
        [HttpPost("Add")]
        public IActionResult Create(BlCustomers customer)
        {
           return Ok(customers.Create(customer));
        }

        // PUT
        [HttpPut("Update")]
        public IActionResult Put(BlCustomers customer)
        {
            return Ok(customers.Update(customer));
        }

        // DELETE 
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            customers.Delete(id);
        }
    }
}
