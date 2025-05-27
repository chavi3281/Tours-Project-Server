using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    internal class BlOrderService : IBlOrder
    {
        IDal dal;
        IBlCustomers customers;
        IBlOrdersDetail detail;
        public BlOrderService(IDal dal, IBlCustomers customers, IBlOrdersDetail detail)
        {
            this.dal = dal;
            this.customers = customers;
            this.detail = detail;
        }

        #region Create
        public async Task Create(BlOrder item)
        {
            Order o = await dal.Order.Create(new Order()
            {
                IdCustomer = item.IdCustomer,
                Price = item.Price,
                Date = item.Date.ToDateTime(TimeOnly.MinValue),
            });
            foreach (var ord in item.OrdersDetails)
            {
                ord.IdOrder = o.Id;
            }
            await detail.Create(item.OrdersDetails);
        }
        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            await dal.Order.Delete(id);
        }
        #endregion

        #region GetAll
        public async Task<List<BlOrder>> GetAll()
        {
            var oList = await dal.Order.GetAll();
            List<BlOrder> list = new();
            var tasks = oList.Select(o => castingOrderFromDalToBl(o));
            var results = await Task.WhenAll(tasks);
            list.AddRange(results);
            return list;
        }
        #endregion

        #region GetById
        public async Task<BlOrder?> GetById(int id)
        {
            Order? o = await dal.Order.GetById(id);
            if (o != null)
                return await castingOrderFromDalToBl(o);
            return null;
        }
        #endregion

        #region castingOrderFromDalToBl
        public async Task<BlOrder> castingOrderFromDalToBl(Order o) =>
            new BlOrder()
            {
                Id = o.Id,
                IdCustomer = o.IdCustomer,
                Date = DateOnly.FromDateTime(o.Date),
                Price = o.Price,
                IdCustomerNavigation = await customers.castingCustomerFromDalToBl(o.IdCustomerNavigation),
                OrdersDetails =  detail.castingOrderDetailFromDalToBl(o.OrdersDetails),
            };
        #endregion

        #region castingOrderFromBlToDal
        public Order castingOrderFromBlToDal(BlOrder o) =>
        new Order()
         {
             Id = o.Id,
             IdCustomer = o.IdCustomer,
             Price = o.Price,
             Date = o.Date.ToDateTime(TimeOnly.MinValue),
         };
        #endregion

        #region GetByCustomerId
        public async Task<List<BlOrder>?> GetByCustomerId(int id)
        {
            var or = await GetAll();
            return or.FindAll(o => o.IdCustomer == id);
        }
        #endregion

        #region GetByClassToFlightId
        public async Task<List<BlOrder>?> GetByClassToFlightId(int id)
        {
            List<BlOrdersDetail>? list = await detail.GetByClassToFlightId(id);
            if (list == null) return null;

            List<BlOrder>? all = await GetAll();
            List<BlOrder>? result = new();

            for (int i = 0; i < all.Count; i++)
            {
                for (int j = 0; j < all[i].OrdersDetails.Count; j++)
                {
                    for (int k = 0; k < list.Count; k++)
                    {
                        BlOrdersDetail o = all[i].OrdersDetails.ToList()[j];
                        if (list[k].Id == o.Id)
                            result.Add(all[i]);
                    }
                }
            }
            return result;
        }
        #endregion
    }
}