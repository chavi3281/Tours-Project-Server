using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class BlOrdersDetailService : IBlOrdersDetail
    {
        IDal dal;
        IBlClassToFlight classToFlight;
        public BlOrdersDetailService(IDal dal, IBlClassToFlight classToFlight)
        {
            this.dal = dal;
            this.classToFlight = classToFlight;
        }

        #region Create
        public async Task Create(ICollection<BlOrdersDetail> ordersDetails)
        {
            foreach (var o in ordersDetails)
            {
                await dal.OrdersDetails.Create(castingOrderDetailsFromBlToDal(o));
                await classToFlight.updateOrderCount(o.IdClassToFlight, o.CountTickets);
            }
        }
        #endregion

        #region Delete
        public async Task<List<BlThisFlight>> Delete(int idThisFlight)
        {
            await dal.OrdersDetails.Delete(idThisFlight);
            return await classToFlight.Delete(idThisFlight);
        }
        #endregion

        #region GetAll
        public async Task<List<BlOrdersDetail>> GetAll()
        {
            var oList = await dal.OrdersDetails.GetAll();
            List<BlOrdersDetail> list = new();
            oList.ForEach(o => list.Add(castingOrderDetailsFromDalToBl(o)));
            return list;
        }
        #endregion

        #region GetById
        public async Task<BlOrdersDetail?> GetById(int id)
        {
            OrdersDetail? o = await dal.OrdersDetails.GetById(id);
            if (o != null)
                return  castingOrderDetailsFromDalToBl(o);
            return null;
        }
        #endregion

        #region castingOrderDetailsFromDalToBl
        public BlOrdersDetail castingOrderDetailsFromDalToBl(OrdersDetail o) =>
            new BlOrdersDetail()
            {
                Id = o.Id,
                Price = o.Price,
                IdOrder = o.OrderId,
                IdClassToFlight = o.IdClassToFlight,
                CountOverLoad = o.CountOverLoad,
                CountTickets = o.CountTickets,
                IdClassToFlightNavigation =  classToFlight.castingclassToFlightFromDalToBl(o.IdClassToFlightNavigation),
            };
        #endregion

        #region castingOrderDetailsFromBlToDal
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
        #endregion

        #region castingOrderDetailFromDalToBl
        public ICollection<BlOrdersDetail> castingOrderDetailFromDalToBl(ICollection<OrdersDetail> item)
        {
            ICollection<BlOrdersDetail> ord = new List<BlOrdersDetail>();
            item.ToList().ForEach(o => { ord.Add(castingOrderDetailsFromDalToBl(o)); });
            return ord;
        }
        #endregion

        #region GetByClassToFlightId
        public async Task<List<BlOrdersDetail>?> GetByClassToFlightId(int id)
        {
            var ctf = await GetAll();
            return ctf.FindAll(o => o.IdClassToFlight == id);
        }
        #endregion
    }
}