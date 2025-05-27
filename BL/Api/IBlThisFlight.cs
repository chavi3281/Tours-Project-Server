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
        Task<List<BlThisFlight>?> GetBySrcDesDate(string src, string des, DateOnly date);
        Task<List<BlThisFlight>?> GetById(int id);
        Task<List<BlThisFlight>> GetAll();
        Task<BlThisFlight> Create(BlThisFlight item);
        Task<List<BlThisFlight>> Delete(int id);
        Task<List<BlThisFlight>> Update(BlThisFlight item);
        Task<BlThisFlight?> castingOver(int id);
    }
}