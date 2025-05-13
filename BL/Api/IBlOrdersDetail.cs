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
        BlOrdersDetail? GetById(int id);
        List<BlOrdersDetail>? GetByClassToFlightId(int id);
        List<BlOrdersDetail> GetAll();

        ICollection<BlOrdersDetail> castingOrderDetailFromDalToBl(ICollection<OrdersDetail> item);
        Task<List<BlThisFlight>> Delete(int id);
        void Create(ICollection<BlOrdersDetail> ordersDetails);
    }
}
