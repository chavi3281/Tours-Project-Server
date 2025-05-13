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
        List<ThisFlight>? GetBySrcDesDate(string src, string des, DateOnly date);
        List<ThisFlight>? GetById(int id);
        List<ThisFlight> GetAll();
        ThisFlight Create(ThisFlight item);
        ThisFlight Update(ThisFlight item);
        Task Delete(int id);
    }
}
