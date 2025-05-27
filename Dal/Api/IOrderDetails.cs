using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IOrderDetails
    {
        Task<OrdersDetail?> GetById(int id);
        Task<List<OrdersDetail>> GetAll();
        Task<OrdersDetail> Create(OrdersDetail item);
        Task Delete(int id);

    }
}
