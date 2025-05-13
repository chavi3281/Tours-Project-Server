
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface ICustomers
    {
        Customer? GetById(string firstName, string lastName, string password);
        List<Customer> GetAll();
        Customer Create(Customer item);
        Customer? Update(Customer item);
        void Delete(int id);

    }
}
