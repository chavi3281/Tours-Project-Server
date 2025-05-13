using Dal.Api;
using Dal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class DalFlightsService : IFlight
    {
        dbcontext db;
        public DalFlightsService(dbcontext db)
        {
            this.db = db;
        }

        public void Create(Flight item)
        {

            db.Flights.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            List<Flight> fList = GetAll();
            db.Remove(fList.Find(f => f.Id == id));
            db.SaveChanges();
        }

        public List<Flight> GetAll() => db.Flights
/*            .Include(x => x.ThisFlights)*/
            .Include(c=> c.DestinationNavigation).Include(a=> a.SourceNavigation).ToList();


        public Flight? GetById(int id) => GetAll().Find(x => x.Id == id);

        public List<Flight> Update(Flight item)
        {
            Flight f = GetAll().Find(x => x.Id == item.Id);
            f.Id = item.Id;
            f.Source = item.Source;
            f.Destination = item.Destination;
            f.TimeOfFlight = item.TimeOfFlight;
            f.Sold = item.Sold;
            db.SaveChanges();
            return GetAll();
        }
    }

}

