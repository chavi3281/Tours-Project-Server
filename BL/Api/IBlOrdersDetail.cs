using BL.Models;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IBlOrdersDetail
    {
        Task<BlOrdersDetail?> GetById(int id);
        Task<List<BlOrdersDetail>?> GetByClassToFlightId(int id);
        Task<List<BlOrdersDetail>> GetAll();
        ICollection<BlOrdersDetail> castingOrderDetailFromDalToBl(ICollection<OrdersDetail> item);
        Task<List<BlThisFlight>> Delete(int id);
        Task Create(ICollection<BlOrdersDetail> ordersDetails);
    }
}
