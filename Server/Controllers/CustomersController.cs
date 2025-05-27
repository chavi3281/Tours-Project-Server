using BL.Api;
using BL.Models;
using Dal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await customers.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: GetById
        [HttpGet("GetById/{firstName}/{lastName}/{password}")]
        public async Task<IActionResult> GetById(string firstName, string lastName, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(password))
                {
                    return BadRequest("First name, last name, and password cannot be null or empty");
                }

                var customer = await customers.GetById(firstName, lastName, password);

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: Add
        [HttpPost("Add")]
        public async Task<IActionResult> Create(BlCustomers customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest("Customer data is null");
                }

                var result = await customers.Create(customer);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT
        [HttpPut("Update")]
        public async Task<IActionResult> Put(BlCustomers customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest("Customer data is null");
                }

                var result = await customers.Update(customer);

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
                    return BadRequest("Invalid customer ID");
                }

                await customers.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
