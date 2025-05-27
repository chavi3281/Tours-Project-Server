using BL.Models;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IBlCustomers
    {
        Task<List<BlCustomers>> GetAll();
        Task<BlCustomers?> GetById(string firstName, string lastName, string password);
        Task<BlCustomers> Create(BlCustomers item);
        Task<BlCustomers?> Update(BlCustomers item);
        Task Delete(int id);
        Task<BlCustomers> castingCustomerFromDalToBl(Customer c);
    }
}