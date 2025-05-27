using Dal.Api;
using Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dal.Services
{
    public class DalThisFlightService : IThisFlight
    {
        dbcontext db;
        public DalThisFlightService(dbcontext db)
        {
            this.db = db;
        }

        #region Create
        public async Task<ThisFlight> Create(ThisFlight item)
        { 
            var t = await db.ThisFlights.AddAsync(item);
            try
            {
               await db.SaveChangesAsync();
                return t.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception("cant saveChanges - ThisFlight" + ex);
            }
        }
        #endregion

        #region Delete
        public async Task<List<ThisFlight>> Delete(int id)
        {
            ThisFlight? tf = (await GetAll()).Find(f => f.Id == id);
            if (tf != null)
            {
                db.Remove(tf);
                try
                {
                    await db.SaveChangesAsync();
                    return await GetAll();
                }
                catch (Exception ex)
                {
                    throw new Exception("cant saveChanges - ThisFlight" + ex);
                }
            }
            return await GetAll();
        }
        #endregion

        #region GetAll
        public async Task<List<ThisFlight>> GetAll() => await db.ThisFlights.Include(x => x.ClassToFlights)
                                                                            .Include(x => x.Flight).ThenInclude(o => o.SourceNavigation).ToListAsync();
        #endregion

        #region GetById
        public async Task<List<ThisFlight>?> GetById(int id) => (await GetAll()).FindAll(x => x.FlightId == id);
        #endregion

        #region GetBySrcDesDate
        public async Task<List<ThisFlight>?> GetBySrcDesDate(string src, string des, DateOnly date)
        {
            DateTime t = date.ToDateTime(TimeOnly.MinValue);
            List<ThisFlight> tf = new();
            if (t < DateTime.Now)
                return null;
            List<Destination> AllDes = await db.Destinations.ToListAsync();
            Destination? s = AllDes.Find(x => x.Destination1 == src);
            Destination? d = AllDes.Find(x => x.Destination1 == des);
            if (s == null || d == null) 
                return null;
            List<Flight> AllFlight = await db.Flights.ToListAsync();
            Flight? flight = AllFlight.Find(f => f.Source == s.Id && f.Destination == d.Id);
            if (flight != null) {
                var allThisFlight = await GetAll();
                tf = allThisFlight.FindAll(f => f.FlightId == flight.Id && DateOnly.FromDateTime(f.Date) == date);
                return tf;
            }
            return tf;
        }
        #endregion

        #region Update
        public async Task<ThisFlight> Update(ThisFlight item)
        {
            ThisFlight? f = (await GetAll()).Find(x => x.Id == item.Id);
            if (f == null) 
                return item;
            f.FlightId = item.FlightId;
            f.Date = item.Date;
            f.PriceToOverLoad = item.PriceToOverLoad;
            try
            {
                await db.SaveChangesAsync();
                return f;
            }
            catch (Exception ex)
            {
                throw new Exception("cant saveChanges - ThisFlight" + ex);
            }
        }
        #endregion

    }
}
