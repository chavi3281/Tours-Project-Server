


using BL.Models;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IBlFlight
    {
        BlFlights? GetById(int id);
        Task<List<BlFlights>> GetAll();
         Task<List<BlFlights>> Create(BlFlights item);
        List<BlFlights> Update(BlFlights item);
        void Delete(int id);
/*        public BlFlights castingFlightFromDalToBl(Flight f);*/
        public Task<BlFlights> castingOver(int f);
        /*        ICollection<BlFlights> castingFlightFromBlToDallist(ICollection<Flight>? flightDestinationNavigations);*/
    }
}
