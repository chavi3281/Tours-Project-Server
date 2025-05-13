using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class BlFlightService : IBlFlight
    {
        IDal dal;
        IBlDestination destination;
        public BlFlightService(IDal dal, IBlDestination destination)
        {
            this.dal = dal;
            this.destination = destination;

        }

        public async Task<List<BlFlights>> Create(BlFlights item)
        {
            List<BlFlights> l = await GetAll();
            BlFlights? bf = l.Find(x => x.Source == item.Source && x.Destination == item.Destination);
            if (bf == null) { 
            Flight flight = new Flight()
            {
                Source = item.Source,
                Destination = item.Destination,
                Sold = item.Sold,
                TimeOfFlight = item.TimeOfFlight,
                
            };
            dal.Flights.Create(flight);
        }
            return await GetAll();
        }



        public async Task<List<BlFlights>> GetAll()
        {
            var fList = dal.Flights.GetAll();
            List<BlFlights> list = new();
            fList.ForEach(f => list.Add(castingFlightFromDalToBl(f)));
            return list;
        }

        public BlFlights castingFlightFromDalToBl(Flight f) => 
                                                            new BlFlights()
                                                            {
                                                                Id = f.Id,
                                                                Source = f.Source,
                                                                Destination = f.Destination,
                                                                Sold = f.Sold,
                                                                TimeOfFlight = f.TimeOfFlight,
                                                                DestinationNavigation = destination.castOver(f.Destination),
                                                                SourceNavigation = destination.castOver(f.Source)
    };


        public List<BlFlights> castingFlightFromDalToBlList(List<Flight> f)
        {
            List<BlFlights> bf = new List<BlFlights>();
            f.ForEach(f => bf.Add(castingFlightFromDalToBl(f)));
            return bf;
        }
                                                    
        public Flight castingFlightFromBlToDal(BlFlights f) =>
                                                                new Flight()
                                                                {
                                                                    Id = f.Id,
                                                                    Source = f.Source,
                                                                    Destination = f.Destination,
                                                                    Sold = f.Sold,
                                                                    TimeOfFlight = f.TimeOfFlight,
                                                                };


        public ICollection<BlFlights>? castingFlightFromBlToDallist(ICollection<Flight> f)
        {
            List<BlFlights> bf = new List<BlFlights>();
            f.ToList().ForEach(f => bf.Add(castingFlightFromDalToBl(f)));
            return bf;
        }
           
        
                                                               


        public BlFlights? GetById(int id)
        {
            Flight? f = dal.Flights.GetById(id);
            return f == null ? null : castingFlightFromDalToBl(f);
        }

        public List<BlFlights> Update(BlFlights item)
        {
            List<Flight> f = dal.Flights.Update(castingFlightFromBlToDal(item));
            return castingFlightFromDalToBlList(f);
        }

        public void Delete(int id)
        {
            dal.Flights.Delete(id);
        }

        public async  Task<BlFlights> castingOver(int f)
        {
            var l = await GetAll();
            BlFlights? fl = l.Find(x => x.Id == f);
            return fl;
        }
    }
}
