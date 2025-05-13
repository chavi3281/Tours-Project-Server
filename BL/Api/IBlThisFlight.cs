using BL.Models;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IBlThisFlight
    {
        List<BlThisFlight>? GetBySrcDesDate(string src, string des, DateOnly date);
        List<BlThisFlight>? GetById(int id);
        List<BlThisFlight> GetAll();
        Task<BlThisFlight> Create(BlThisFlight item);
        Task<List<BlThisFlight>> Delete(int id);
         List<BlThisFlight> Update(BlThisFlight item);

        public BlThisFlight castingOver(int id);

    }
}
