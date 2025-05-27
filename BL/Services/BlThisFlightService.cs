using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BL.Services
{
    public class BlThisFlightService : IBlThisFlight
    {

        IDal dal;
        IBlFlight flight;

        public BlThisFlightService(IDal dal, IBlFlight flight)
        {
            this.dal = dal;
            this.flight = flight;
        }

        #region Create
        public async Task<BlThisFlight> Create(BlThisFlight item)
        {
            ThisFlight tf = new()
            {
              FlightId = item.FlightId,
              Date = item.Date.ToDateTime(item.Time),
              PriceToOverLoad = item.PriceToOverLoad,
            };
            return  castingThisFlightFromDalToBl(await dal.ThisFlight.Create(tf));
            
        }
        #endregion

        #region Delete
        public async Task<List<BlThisFlight>> Delete(int id)
        {
           List<ThisFlight> tf =  await dal.ThisFlight.Delete(id);
            return castingThisFlightListFromDalToBl(tf);
        }
        #endregion

        #region GetAll
        public async Task<List<BlThisFlight>> GetAll()
        {
            var tfList = await dal.ThisFlight.GetAll();
            List<BlThisFlight> bltf = castingThisFlightListFromDalToBl(tfList);
            return bltf;
        }
        #endregion

        #region castingThisFlightFromDalToBl
        public BlThisFlight castingThisFlightFromDalToBl(ThisFlight tf) =>
                                                            new BlThisFlight()
                                                            {
                                                                Id = tf.Id,
                                                                Date = DateOnly.FromDateTime(tf.Date),
                                                                Time = TimeOnly.FromDateTime(tf.Date),
                                                                FlightId = tf.FlightId,
                                                                PriceToOverLoad = tf.PriceToOverLoad,
                                                                Flight =  flight.castingOver(tf.FlightId).Result};
        #endregion

        #region castingOver
        public async Task<BlThisFlight?> castingOver(int id)
        {
            BlThisFlight? f =(await GetAll()).Find(f => f.Id == id);
            return f;
        }
        #endregion

        #region castingThisFlightListFromDalToBl
        public List<BlThisFlight> castingThisFlightListFromDalToBl(List<ThisFlight> tf)
        {
            List<BlThisFlight> list = new();
            tf.ToList().ForEach( f => list.Add(castingThisFlightFromDalToBl(f)));
            return list;
        }
        #endregion

        #region castingThisFlightFromBlToDal
        public ThisFlight castingThisFlightFromBlToDal(BlThisFlight tf) =>
                                                                new ThisFlight()
                                                                {
                                                                    Id = tf.Id,
                                                                    Date = tf.Date.ToDateTime(tf.Time),
                                                                    FlightId = tf.FlightId,
                                                                    PriceToOverLoad = tf.PriceToOverLoad,
                                                                };
        #endregion

        #region GetBySrcDesDate
        public async Task<List<BlThisFlight>?> GetBySrcDesDate(string src, string des, DateOnly date)
        {
            List<ThisFlight>? lst = await dal.ThisFlight.GetBySrcDesDate(src, des, date);
            if(lst != null)
                return  castingThisFlightListFromDalToBl(lst);
            return null;
        }
        #endregion

        #region Update
        public async Task<List<BlThisFlight>> Update(BlThisFlight item)
        {
            ThisFlight tf = await dal.ThisFlight.Update( castingThisFlightFromBlToDal(item));
            return await GetAll();
        }
        #endregion

        #region castingFlightFromBlToDallist
        public ICollection<BlThisFlight>? castingFlightFromBlToDallist(ICollection<ThisFlight> f)
        {
            List<BlThisFlight> bf = new List<BlThisFlight>();
            f.ToList().ForEach( f => bf.Add(castingThisFlightFromDalToBl(f)));
            return  bf;
        }
        #endregion

        #region GetById
        public async Task<List<BlThisFlight>?> GetById(int id)
        {
            List<ThisFlight>? tf = await dal.ThisFlight.GetById(id);
            if(tf != null)
            return  castingThisFlightListFromDalToBl(tf);
            return null;
        }
        #endregion

    }
}