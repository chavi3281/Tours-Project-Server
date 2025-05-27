using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IThisFlight
    {
        Task<List<ThisFlight>?> GetBySrcDesDate(string src, string des, DateOnly date);
        Task<List<ThisFlight>?> GetById(int id);
        Task<List<ThisFlight>> GetAll();
        Task<ThisFlight> Create(ThisFlight item);
        Task<ThisFlight> Update(ThisFlight item);
        Task<List<ThisFlight>> Delete(int id);
    }
}
