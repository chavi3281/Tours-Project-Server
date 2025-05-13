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

        public BlThisFlightService(IDal dal, IBlFlight flight/*, IBlClassToFlight classToFlight*/)
        {
            this.dal = dal;
            this.flight = flight;
        }

        public Task<BlThisFlight> Create(BlThisFlight item)
        {
            ThisFlight tf = new()
            {
              FlightId = item.FlightId,
              Date = item.Date.ToDateTime(item.Time),
              PriceToOverLoad = item.PriceToOverLoad,
            };
            return castingThisFlightFromDalToBl(dal.ThisFlight.Create(tf));
            
        }

        public async Task<List<BlThisFlight>> Delete(int id)
        {
            await dal.ThisFlight.Delete(id);
            return GetAll();
        }

        public List<BlThisFlight> GetAll()
        {
            var tfList = dal.ThisFlight.GetAll();
            List<BlThisFlight> list = new();
            tfList.ForEach(async tf => list.Add( await castingThisFlightFromDalToBl(tf)));
            return list;
        }

        public async Task<BlThisFlight> castingThisFlightFromDalToBl(ThisFlight tf) =>
                                                            new BlThisFlight()
                                                            {
                                                                Id = tf.Id,
                                                                Date = DateOnly.FromDateTime(tf.Date),
                                                                Time = TimeOnly.FromDateTime(tf.Date),
                                                                FlightId = tf.FlightId,
                                                                PriceToOverLoad = tf.PriceToOverLoad,
                                                                Flight = await flight.castingOver(tf.FlightId)
    };

        public BlThisFlight castingOver(int id)
        {
            BlThisFlight? f = GetAll().Find(f => f.Id == id);
            return f;
        }

        public List<BlThisFlight> castingThisFlightListFromDalToBl(List<ThisFlight> tf)
        {
            List<BlThisFlight> list = new();
            tf.ToList().ForEach(async f => list.Add(await castingThisFlightFromDalToBl(f)));
            return list;
        }
                                                   

        public ThisFlight castingThisFlightFromBlToDal(BlThisFlight tf) =>
                                                                new ThisFlight()
                                                                {
                                                                    Id = tf.Id,
                                                                    Date = tf.Date.ToDateTime(tf.Time),
                                                                    FlightId = tf.FlightId,
                                                                    PriceToOverLoad = tf.PriceToOverLoad,
                                                                };

        public List<BlThisFlight>? GetBySrcDesDate(string src, string des, DateOnly date)
        {
            List<ThisFlight>? lst = dal.ThisFlight.GetBySrcDesDate(src, des, date);
            if(lst != null)
                return castingThisFlightListFromDalToBl(lst);
            return null;
        }

        public List<BlThisFlight> Update(BlThisFlight item)
        {
            ThisFlight tf = dal.ThisFlight.Update(castingThisFlightFromBlToDal(item));
            return GetAll();
        }

        public ICollection<BlThisFlight>? castingFlightFromBlToDallist(ICollection<ThisFlight> f)
        {
            List<BlThisFlight> bf = new List<BlThisFlight>();
            f.ToList().ForEach(async f => bf.Add(await castingThisFlightFromDalToBl(f)));
            return bf;
        }


        public List<BlThisFlight>? GetById(int id)
        {
            List<ThisFlight>? tf = dal.ThisFlight.GetById(id);
            if(tf != null)
            return castingThisFlightListFromDalToBl(tf);
            return null;
        }

    }
}