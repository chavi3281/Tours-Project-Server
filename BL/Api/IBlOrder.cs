using BL.Models;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IBlOrder
    {
        Task<BlOrder?> GetById(int id);
        Task<List<BlOrder>?> GetByCustomerId(int id);

        Task<List<BlOrder>> GetAll();
        Task Create(BlOrder item);
        Task Delete(int id);
        Task<List<BlOrder>?> GetByClassToFlightId(int id);
    }

}
