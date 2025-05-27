using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IOrder
    {
        Task<Order?> GetById(int id);
        Task<List<Order>> GetAll();
        Task<Order> Create(Order item);
        Task Delete(int id);

    }
}
