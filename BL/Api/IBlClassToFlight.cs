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
        BlClassToFlight? GetByClassFlightId(string cl, int fligth);
        List<BlClassToFlight> GetAllSales();
        List<BlClassToFlight> GetAll();
        void Create(BlClassToFlight item);
        List<BlClassToFlight> Update(BlClassToFlight item);
        Task<List<BlThisFlight>> Delete(int id);
        public BlClassToFlight? castingclassToFlightFromDalToBl(ClassToFlight f);
        public void updateOrderCount(int f, int cnt);


        public ICollection<BlClassToFlight>? castingClassToFlightFromDalToBllist(ICollection<ClassToFlight> f);
    }
}
