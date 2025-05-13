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

        public async void Create(BlOrder item)
        {
            Order o = await dal.Order.Create(new Order()
            {
                IdCustomer = item.IdCustomer,
                Price = item.Price,
                Date = item.Date.ToDateTime(TimeOnly.MinValue),
            });

            item.OrdersDetails.ToList().ForEach(ord => ord.IdOrder = o.Id);


            detail.Create(item.OrdersDetails);
        }

        public void Delete(int id)
        {
            dal.Order.Delete(id);
        }

        public List<BlOrder> GetAll()
        {
            var oList = dal.Order.GetAll();
            List<BlOrder> list = new();
            oList.ForEach(o => list.Add(castingOrderFromDalToBl(o)));
            return list;
        }

        public BlOrder? GetById(int id)
        {
            Order? o = dal.Order.GetById(id);
            if(o != null)
            return castingOrderFromDalToBl(o);
            return null;

        }

        public BlOrder castingOrderFromDalToBl(Order o) =>
                                                            new BlOrder()
                                                            {
                                                                Id = o.Id,
                                                                IdCustomer = o.IdCustomer,
                                                                Date = DateOnly.FromDateTime(o.Date),
                                                                Price = o.Price,
                                                                IdCustomerNavigation = customers.castingCustomerFromDalToBl(o.IdCustomerNavigation),
                                                                OrdersDetails = detail.castingOrderDetailFromDalToBl(o.OrdersDetails),
                                                            };

        public Order castingOrderFromBlToDal(BlOrder o) =>
                                                                new Order()
                                                                {
                                                                    Id = o.Id,
                                                                    IdCustomer = o.IdCustomer,
                                                                    Price = o.Price,
                                                                    Date = o.Date.ToDateTime(TimeOnly.MinValue),
                                                                };

        public List<BlOrder>? GetByCustomerId(int id)
        {
            return GetAll().FindAll(o => o.IdCustomer == id);
        }

        public List<BlOrder>? GetByClassToFlightId(int id)
        {
            List<BlOrdersDetail>? list = detail.GetByClassToFlightId(id);
            if(list == null) return null;
            List<BlOrder>? all = GetAll();
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
    }
}
            