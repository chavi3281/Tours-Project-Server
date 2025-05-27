using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IClassToFlight
    {
        Task<ClassToFlight?> GetByClassFlightId(string classs, int flightId);
        Task<List<ClassToFlight>> GetAll();
        Task Create(ClassToFlight item);
        Task<List<ClassToFlight>> Update(ClassToFlight item);

        Task<List<ClassToFlight>> GetAllSales();
        Task Delete(int id);
    }
}
