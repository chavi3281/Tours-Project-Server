using BL.Models;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IBlClassToFlight
    {
        Task<BlClassToFlight?> GetByClassFlightId(string cl, int fligth);
        Task<List<BlClassToFlight>> GetAllSales();
        Task<List<BlClassToFlight>> GetAll();

        Task Create(BlClassToFlight item);
        Task<List<BlClassToFlight>> Update(BlClassToFlight item);
        Task<List<BlThisFlight>> Delete(int id);
        BlClassToFlight? castingclassToFlightFromDalToBl(ClassToFlight f);
        Task updateOrderCount(int f, int cnt);
         ICollection<BlClassToFlight>? castingClassToFlightFromDalToBllist(ICollection<ClassToFlight> f);
    }
}
