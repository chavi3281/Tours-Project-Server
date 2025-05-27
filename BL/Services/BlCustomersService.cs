using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    //הפעולות של השרת
    internal class BlCustomersService : IBlCustomers
    {
        IDal dal;
        public BlCustomersService(IDal dal)
        {
            this.dal = dal;
        }

        #region Create
        public async Task<BlCustomers?> Create(BlCustomers item)
        {
            if(item.FirstName == null) 
                throw new ArgumentNullException(nameof(item.FirstName));
            BlCustomers? c = await GetById(item.FirstName, item.LastName, item.Password);
            if (c == null)
            {
                Customer customer = new Customer()
                {
                    Password = item.Password,
                    IsManager = item.IsManager,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    Phone = item.Phone
                };
                customer = await dal.Customers.Create(customer);
                return await castingCustomerFromDalToBl(customer);
            }
            return null;
        }
        #endregion

        #region GetAll
        public async Task<List<BlCustomers>> GetAll()
        {
            var cList = await dal.Customers.GetAll();
            List<BlCustomers> list = new();
            cList.ForEach(async c => list.Add(await castingCustomerFromDalToBl(c)));
            return list;
        }
        #endregion

        #region castingCustomerFromBlToDal
        public Task<Customer> castingCustomerFromBlToDal(BlCustomers item) =>
            Task.FromResult(new Customer()
            {
                Id = item.Id,
                Password = item.Password,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                Phone = item.Phone,
                IsManager = item.IsManager
            });
        #endregion

        #region castingCustomerFromDalToBl
        public Task<BlCustomers> castingCustomerFromDalToBl(Customer item) =>
            Task.FromResult(new BlCustomers()
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                Phone = item.Phone,
                Password = item.Password,
                IsManager = item.IsManager
            });
        #endregion

        #region GetById
        public async Task<BlCustomers?> GetById(string firstName, string lastName, string password)
        {
            Customer? c = await dal.Customers.GetById(firstName, lastName, password);
            if (c == null)
                return null;
            return  await castingCustomerFromDalToBl(c);
        }
        #endregion

        #region Update
        public async Task<BlCustomers?> Update(BlCustomers item)
        {
            Customer? c = await castingCustomerFromBlToDal(item);
            c = await dal.Customers.Update(c);
            if (c == null) return null;
            return  await castingCustomerFromDalToBl(c);
        }
        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            await dal.Customers.Delete(id);
        }
        #endregion
    }
}