using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IFlight
    {
        Task<Flight?> GetById(int id);
        Task<List<Flight>> GetAll();
        Task Create(Flight item);
        Task<List<Flight>> Update(Flight item);
        Task Delete(int id);
    }
}
