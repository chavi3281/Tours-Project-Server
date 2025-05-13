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

        public BlCustomers Create(BlCustomers item)
        {
            BlCustomers? c = GetById(item.FirstName, item.LastName, item.Password);
            if (c == null)
            {
                //יצירת אוביקט מסוג לקוח ומעתיק אליו את הנתונים
                Customer customer = new Customer()
            {
                Password = item.Password,
                IsManager = item.IsManager,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                Phone = item.Phone
            };
            //שולח לשרת ליצור את האוביקט החדש
            customer =  dal.Customers.Create(customer);
            return castingCustomerFromDalToBl(customer);
            }
            return null;
        }

        public List<BlCustomers> GetAll()
        {
            // מעביר את הפרמטרים הרצויים לרשימה
            var cList = dal.Customers.GetAll();
            List<BlCustomers> list = new();
            cList.ForEach(c => list.Add(castingCustomerFromDalToBl(c)));
            return list;
        }

        public Customer castingCustomerFromBlToDal(BlCustomers item) => new Customer()
                                                                        {
                                                                            Id = item.Id,
                                                                            Password = item.Password,
                                                                            FirstName = item.FirstName,
                                                                            LastName = item.LastName,
                                                                            Email = item.Email,
                                                                            Phone = item.Phone,
                                                                            IsManager = item.IsManager
        };

        public BlCustomers castingCustomerFromDalToBl(Customer item) => new BlCustomers()        
                                                                        {
                                                                            Id = item.Id,
                                                                            FirstName = item.FirstName,
                                                                            LastName = item.LastName,
                                                                            Email = item.Email,
                                                                            Phone = item.Phone,
                                                                            Password = item.Password,
                                                                            IsManager = item.IsManager
        };
        public BlCustomers? GetById(string firstName, string lastName, string password)
        {
            Customer c = dal.Customers.GetById(firstName, lastName, password);
            if (c == null) 
                return null;
            return castingCustomerFromDalToBl(c);
        }

        public BlCustomers? Update(BlCustomers item) {
            Customer c = castingCustomerFromBlToDal(item);
            c = dal.Customers.Update(c);
            return castingCustomerFromDalToBl(c);
        }

        public void Delete(int id)
        {
            dal.Customers.Delete(id);

        }

     
    }
}
