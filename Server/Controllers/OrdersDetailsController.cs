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
    public class OrdersDetailsController : ControllerBase
    {
        IBlOrdersDetail ordersDetails;

        public OrdersDetailsController(Ibl bl)
        {
            this.ordersDetails = bl.OrderDetails;
        }

        // GET: GetAll
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(ordersDetails.GetAll());
        }

        // GET: GetById
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(ordersDetails.GetById(id));
        }





        // POST: Add
        [HttpPost("Add")]
        public void Create(List<BlOrdersDetail> od)
        {
            ordersDetails.Create(od);
        }



        // DELETE 
        [HttpDelete("{idThisFlight}")]
        public async Task<List<BlThisFlight>> Delete(int idThisFlight)
        {
            return await ordersDetails.Delete(idThisFlight);
        }
    }
}
