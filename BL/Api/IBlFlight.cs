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
        Task<BlFlights?> GetById(int id);
        Task<List<BlFlights>> GetAll();
        Task<List<BlFlights>> Create(BlFlights item);
        Task<List<BlFlights>> Update(BlFlights item);
        Task Delete(int id);
        Task<BlFlights> castingOver(int f);
    }
}