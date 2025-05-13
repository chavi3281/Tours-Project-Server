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
    public class DalOrdersDetailService : IOrderDetails
    {
        dbcontext dbcontext;
        public DalOrdersDetailService(dbcontext data)
        {
            this.dbcontext = data;
        }

        public OrdersDetail Create(OrdersDetail item)
        {
            dbcontext.Add(item);
            dbcontext.SaveChanges();
            return item;
        }

        public List<OrdersDetail> GetAll()
        {
            return dbcontext.OrdersDetails.Include(x => x.Order)
                                          .Include(x => x.IdClassToFlightNavigation).ThenInclude(c => c.Class)
                                          .Include(x => x.IdClassToFlightNavigation).ThenInclude(f => f.Thisflight).ToList();
        }
        public async Task Delete(int id)
        {
             List<OrdersDetail> olist =  GetAll();
            OrdersDetail? o = olist.Find(d => d.IdClassToFlightNavigation.ThisflightId == id);
            if(o != null) { 
            dbcontext.Remove(o);
            dbcontext.SaveChangesAsync();}
        }
        public OrdersDetail? GetById(int id)
        {
            OrdersDetail? o = GetAll().Find(o => o.Id == id);
            return o;
        }
    }
}

