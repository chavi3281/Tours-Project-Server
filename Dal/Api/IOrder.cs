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
        Order? GetById(int id);
        List<Order> GetAll();
        Task<Order> Create(Order item);
        void Delete(int id);

    }
}
