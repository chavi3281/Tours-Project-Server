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
        ClassToFlight? GetByClassFlightId(string classs, int flightId);
        List<ClassToFlight> GetAll();
        void Create(ClassToFlight item);
        List<ClassToFlight> Update(ClassToFlight item);

        List<ClassToFlight> GetAllSales();
        Task Delete(int id);
    }
}
