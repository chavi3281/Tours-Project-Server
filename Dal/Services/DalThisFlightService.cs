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

        public ThisFlight Create(ThisFlight item)
        {
            var t = db.ThisFlights.Add(item);
                db.SaveChanges();
            return t.Entity;

            
        }

        public async Task Delete(int id)
        {
            List<ThisFlight> fList = GetAll();
            db.Remove(fList.Find(f => f.Id == id));
            await db.SaveChangesAsync();
        }

        public List<ThisFlight> GetAll() => db.ThisFlights.Include(x => x.ClassToFlights).Include(x => x.Flight).ThenInclude(o => o.SourceNavigation).ToList();

        public List<ThisFlight>? GetById(int id)
        {
            return GetAll().FindAll(x => x.FlightId == id);
        }

        public List<ThisFlight>? GetBySrcDesDate(string src, string des, DateOnly date)
        {
            DateTime t = date.ToDateTime(TimeOnly.MinValue);
            List<ThisFlight> tf = new();
            if (t < DateTime.Now)
                return tf;
            Destination? s = db.Destinations.ToList().Find(x => x.Destination1 == src);
            Destination? d = db.Destinations.ToList().Find(x => x.Destination1 == des);
            Flight? flight = db.Flights.ToList().Find(f => f.Source == s.Id && f.Destination == d.Id);
            if (flight != null) {
                tf = GetAll().FindAll(f => f.FlightId == flight.Id && DateOnly.FromDateTime(f.Date) == date);
                return tf;
            }
            return tf;
        }
 

        public ThisFlight Update(ThisFlight item)
        {
            ThisFlight f = GetAll().Find(x => x.Id == item.Id);
            f.FlightId = item.FlightId;
            f.Date = item.Date;
            f.PriceToOverLoad = item.PriceToOverLoad;
            db.SaveChanges();
            return f;
        }

    }
}
