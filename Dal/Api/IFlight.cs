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
        Flight? GetById(int id);
        List<Flight> GetAll();
        void Create(Flight item);
        List<Flight> Update(Flight item);
        void Delete(int id);
    }
}
