using Dal.Api;
using Dal.Models;
using Microsoft.EntityFrameworkCore;
//using Dal.Do;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        #region Create
        public async Task<Customer> Create(Customer item)
        {
            var cc = await dbcontext.Customers.AddAsync(item);
            try
            {
                await dbcontext.SaveChangesAsync();
                return cc.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception("cant saveChanges - Customer" + ex);
            }
        }
        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            var cus = (await GetAll()).Find(c => c.Id == id);
            if(cus != null) { 
            dbcontext.Remove(cus);
                try
                {
                    await dbcontext.SaveChangesAsync();
                } catch (Exception ex)
                {
                    throw new Exception("cant saveChanges - Customer" + ex);
                }
            }
        }
        #endregion

        #region GetAll
        public async Task<List<Customer>> GetAll() => await dbcontext.Customers.ToListAsync();
        #endregion

        #region GetById
        public async Task<Customer?> GetById(string firstName, string lastName, string password) => (await GetAll()).Find(x => x.FirstName == firstName && x.LastName == lastName && x.Password == password);
        #endregion

        #region Update
        public async Task<Customer?> Update(Customer item)
        {
            Customer? customer = (await GetAll()).Find(x => x.Id == item.Id);
            if(customer == null) 
                return null;
            customer.FirstName = item.FirstName;
            customer.LastName = item.LastName;
            customer.Password = item.Password;
            customer.Email = item.Email;
            customer.Phone = item.Phone;
            customer.IsManager = item.IsManager;
            try
            {
                await dbcontext.SaveChangesAsync();
                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception("cant saveChanges - Customer" + ex);
            }
        }
        #endregion

    }
}
