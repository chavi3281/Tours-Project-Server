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

        #region Create
        public async Task<OrdersDetail> Create(OrdersDetail item)
        {
           await dbcontext.AddAsync(item);

            try
            {
               await dbcontext.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception("cant saveChanges - orderDetails" + ex);
            }
        }
        #endregion

        #region GetAll
        public async Task<List<OrdersDetail>> GetAll()
        {
            return await dbcontext.OrdersDetails.Include(x => x.Order)
                                                .Include(x => x.IdClassToFlightNavigation).ThenInclude(c => c.Class)
                                                .Include(x => x.IdClassToFlightNavigation).ThenInclude(f => f.Thisflight).ToListAsync();
        }

        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            List<OrdersDetail>? o = (await GetAll()).FindAll(d => d.IdClassToFlightNavigation.ThisflightId == id);
            if(o != null) { 
            dbcontext.RemoveRange(o);
            try { 
             await dbcontext.SaveChangesAsync();
            }catch (Exception ex)
            {
                    throw new Exception("cant saveChanges - orderDetails" + ex);
            }
            }
        }
        #endregion

        #region GetById
        public async Task<OrdersDetail?> GetById(int id) => (await GetAll()).Find(o => o.Id == id);
        #endregion
    }
}

