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
        OrdersDetail? GetById(int id);
        List<OrdersDetail> GetAll();
        OrdersDetail Create(OrdersDetail item);
        Task Delete(int id);

    }
}
