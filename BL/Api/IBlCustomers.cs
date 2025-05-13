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
        List<BlCustomers> GetAll();
        BlCustomers? GetById(string firstName, string lastName, string password);
        BlCustomers Create(BlCustomers item);
        BlCustomers? Update(BlCustomers item);
        void Delete(int id);
        public BlCustomers castingCustomerFromDalToBl(Customer c);
    }
}
