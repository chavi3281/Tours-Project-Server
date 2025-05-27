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

        #region Create
        public async Task<List<BlFlights>> Create(BlFlights item)
        {
            BlFlights? bf = (await GetAll()).Find(x => x.Source == item.Source && x.Destination == item.Destination);
            if (bf == null)
            {
                Flight flight = new Flight()
                {
                    Source = item.Source,
                    Destination = item.Destination,
                    Sold = item.Sold,
                    TimeOfFlight = item.TimeOfFlight,
                };
                await dal.Flights.Create(flight);
            }
            return await GetAll();
        }
        #endregion

        #region GetAll
        public async Task<List<BlFlights>> GetAll()
        {
            var fList = await dal.Flights.GetAll();
            List<BlFlights> list =  castingFlightFromDalToBlList(fList);
            return list;
        }
        #endregion

        #region castingFlightFromDalToBl
        public BlFlights castingFlightFromDalToBl(Flight f) =>
            new BlFlights()
            {
                Id = f.Id,
                Source = f.Source,
                Destination = f.Destination,
                Sold = f.Sold,
                TimeOfFlight = f.TimeOfFlight,
                DestinationNavigation =  destination.castOver(f.Destination).Result,
                SourceNavigation =  destination.castOver(f.Source).Result
            };
        #endregion

        #region castingFlightFromDalToBlList
        public List<BlFlights> castingFlightFromDalToBlList(List<Flight> f)
        {
            List<BlFlights> bf = new List<BlFlights>();
            f.ForEach( f => bf.Add( castingFlightFromDalToBl(f)));
            return bf;
        }
        #endregion

        #region castingFlightFromBlToDal
        public Flight castingFlightFromBlToDal(BlFlights f) =>
            new Flight()
            {
                Id = f.Id,
                Source = f.Source,
                Destination = f.Destination,
                Sold = f.Sold,
                TimeOfFlight = f.TimeOfFlight,
            };
        #endregion

        #region castingFlightFromBlToDallist
        public ICollection<BlFlights>? castingFlightFromBlToDallist(ICollection<Flight> f)
        {
            List<BlFlights> bf = new List<BlFlights>();
            f.ToList().ForEach(f => bf.Add(castingFlightFromDalToBl(f)));
            return bf;
        }
        #endregion

        #region GetById
        public async Task<BlFlights?> GetById(int id)
        {
            Flight? f = await dal.Flights.GetById(id);
            return f == null ? null :  castingFlightFromDalToBl(f);
        }
        #endregion

        #region Update
        public async Task<List<BlFlights>> Update(BlFlights item)
        {
            List<Flight> f = await dal.Flights.Update(castingFlightFromBlToDal(item));
            return castingFlightFromDalToBlList(f);
        }
        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            await dal.Flights.Delete(id);
        }
        #endregion

        #region castingOver
        public async Task<BlFlights> castingOver(int f)
        {
            var l = await GetAll();
            BlFlights? fl = l.Find(x => x.Id == f);
            if (fl != null)
                return fl;
            else throw new Exception("id: " + f + " dont exist");
        }
        #endregion
    }
}