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

        #region Create
        public async Task<Order> Create(Order item)
        {
           var cc = await dbcontext.Orders.AddAsync(item);
            try
            {
                await dbcontext.SaveChangesAsync();
                return cc.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception("cant saveChanges - Order" + ex);
            }
        }
        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            List<Order>? olist = (await GetAll()).FindAll(d => d.Id == id);
            if (olist != null)
            {
                dbcontext.RemoveRange(olist);
                try
                {
                    await dbcontext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("cant saveChanges - Customer" + ex);
                }
            }
        }
        #endregion

        #region GetAll
        public async Task<List<Order>> GetAll() => await dbcontext.Orders.Include(a => a.IdCustomerNavigation)
                                                                         .Include(a => a.OrdersDetails).ThenInclude(c => c.IdClassToFlightNavigation)
                                                                                                       .ThenInclude(cl => cl.Class).ToListAsync();
        #endregion

        #region
        public async Task<Order?> GetById(int id) => (await GetAll()).Find(ord => ord.Id == id);
        #endregion
    }
}
