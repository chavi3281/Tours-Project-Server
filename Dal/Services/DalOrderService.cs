using Dal.Api;
using Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class DalOrderService : IOrder
    {

        dbcontext dbcontext;
        public DalOrderService(dbcontext data)
        {
            dbcontext = data;
        }

        public async Task<Order> Create(Order item)
        {
           var cc= await dbcontext.Orders.AddAsync(item);
           await dbcontext.SaveChangesAsync();
            return cc.Entity;
        }

        public void Delete(int id)
        {
            List<Order>? olist = GetAll().FindAll(d => d.Id == id);
            if(olist != null) { 
            dbcontext.RemoveRange(olist);
            dbcontext.SaveChanges();}
        }

        public List<Order> GetAll() => dbcontext.Orders.Include(a => a.IdCustomerNavigation).Include(a => a.OrdersDetails).ThenInclude(c => c.IdClassToFlightNavigation).ThenInclude(cl => cl.Class).ToList();
    

        public Order? GetById(int id)
        {
           Order? o = GetAll().Find(o => o.Id == id);
            return o;
        }

    }
}
