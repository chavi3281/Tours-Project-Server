using Dal.Api;
using Dal.Models;
//using Dal.Do;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class DalCustomersServices : ICustomers
    {
        dbcontext dbcontext;
        public DalCustomersServices(dbcontext data)
        {
            dbcontext = data;
        }

        public Customer Create(Customer item)
        {
            dbcontext.Customers.Add(item);
            dbcontext.SaveChanges();
            return  GetById(item.FirstName, item.LastName, item.Password);

        }

        public void Delete(int id)
        {
            List<Customer> cList = GetAll();
            dbcontext.Remove(cList.Find(c => c.Id == id));
            dbcontext.SaveChanges();
        }

        public List<Customer> GetAll() => dbcontext.Customers.ToList();


        public Customer? GetById(string firstName, string lastName, string password) => GetAll().Find(x => x.FirstName == firstName && x.LastName == lastName && x.Password == password);

        public Customer? Update(Customer item)
        {
            Customer customer = GetAll().Find(x => x.Id == item.Id);
            customer.FirstName = item.FirstName;
            customer.LastName = item.LastName;
            customer.Password = item.Password;
            customer.Email = item.Email;
            customer.Phone = item.Phone;
            customer.IsManager = item.IsManager;
            Console.WriteLine(customer.FirstName);
            dbcontext.SaveChanges();
            return customer;    
        }


    }
}
