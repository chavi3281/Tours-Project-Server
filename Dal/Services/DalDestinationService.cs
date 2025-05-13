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
    public class DalDestinationService : IDestination
    {
        dbcontext dbcontext;
        public DalDestinationService(dbcontext data)
        {
            this.dbcontext = data;
        }

        public List<Destination> Create(Destination item)
        {
            dbcontext.Destinations.Add(item);
            dbcontext.SaveChanges();
            return GetAll();

        }

        public void Delete(int destination)
        {
            List<Destination> dlist = GetAll();
            dbcontext.Remove(dlist.Find(d => d.Id == destination));
            dbcontext.SaveChanges();
        }

        public List<Destination> GetAll() {
            if (dbcontext == null) { 
                dbcontext = new dbcontext();
             return   dbcontext.Destinations.Include(d => d.FlightSourceNavigations).Include(d => d.FlightDestinationNavigations).ToList();
            }
            else
            {
                return dbcontext.Destinations.Include(d => d.FlightSourceNavigations).Include(d => d.FlightDestinationNavigations).ToList();

            }
        } 
            


        public Destination? GetById(string destination) => GetAll().Find(d => d.Destination1 == destination);



        public Destination? Update(Destination item)
        {
            Destination? d = GetAll().Find(d => d.Id == item.Id);
            d.Destination1 = item.Destination1;
            d.Path = item.Path;
            dbcontext.SaveChanges();
            return d;
        }

    }
}
