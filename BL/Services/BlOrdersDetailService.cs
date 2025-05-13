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
    public class BlOrdersDetailService: IBlOrdersDetail
    {
        IDal dal;
        IBlClassToFlight classToFlight;

        public BlOrdersDetailService(IDal dal, IBlClassToFlight classToFlight)
        {
            this.dal = dal;
            this.classToFlight = classToFlight;
        }

        public async void Create(ICollection<BlOrdersDetail> ordersDetails)
        {
            ordersDetails.ToList().ForEach(o => { 
                dal.OrdersDetails.Create(castingOrderDetailsFromBlToDal(o));
                classToFlight.updateOrderCount(o.IdClassToFlight, o.CountTickets);
                }
                ) ;

        }

        public async Task<List<BlThisFlight>> Delete(int idThisFlight)
        {
              await dal.OrdersDetails.Delete(idThisFlight);
               return await classToFlight.Delete(idThisFlight);

        }

        public List<BlOrdersDetail> GetAll()
        {
            var oList = dal.OrdersDetails.GetAll();
            List<BlOrdersDetail> list = new();
            oList.ForEach(o => list.Add(castingOrderDetailsFromDalToBl(o)));
            return list;
        }

        public BlOrdersDetail? GetById(int id)
        {
            OrdersDetail? o = dal.OrdersDetails.GetById(id);
            if (o != null) 
            return castingOrderDetailsFromDalToBl(o);
            return null;
        }

        public BlOrdersDetail castingOrderDetailsFromDalToBl(OrdersDetail o) =>
                                                           new BlOrdersDetail()
                                                           {
                                                               Id = o.Id,
                                                               Price = o.Price,
                                                               IdOrder = o.OrderId,
                                                               IdClassToFlight = o.IdClassToFlight,
                                                               CountOverLoad = o.CountOverLoad,
                                                               CountTickets = o.CountTickets,
                                                               IdClassToFlightNavigation = classToFlight.castingclassToFlightFromDalToBl(o.IdClassToFlightNavigation),
                                                           };

        public OrdersDetail castingOrderDetailsFromBlToDal(BlOrdersDetail o) =>
                                                                new OrdersDetail()
                                                                {
                                                                    Id = o.Id,
                                                                    OrderId = o.IdOrder,
                                                                    IdClassToFlight = o.IdClassToFlight,
                                                                    Price = o.Price,
                                                                    CountOverLoad = o.CountOverLoad,
                                                                    CountTickets = o.CountTickets
                                                                };

        public ICollection<BlOrdersDetail> castingOrderDetailFromDalToBl(ICollection<OrdersDetail> item)
        {
            List<BlOrdersDetail> ordersDetails = new List<BlOrdersDetail>();
            item.ToList().ForEach(o => { ordersDetails.Add(castingOrderDetailsFromDalToBl(o)); });
            return ordersDetails ;
        }

        public List<BlOrdersDetail>? GetByClassToFlightId(int id)
        {
            return GetAll().FindAll(o => o.IdClassToFlight == id);
        }
    }
}
