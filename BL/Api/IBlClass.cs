using BL.Models;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IBlClass
    {
        Task<List<BlClass>> GetAll();
        Task<BlClass?> GetById(int id);
        Task<List<BlClass>> Create(BlClass item);
        Task<List<BlClass>> Update(BlClass item);
        Task<List<BlClass>> Delete(string description);
        BlClass castingClassFromDalToBl(Class c);
        Class castingClassFromBlToDal(BlClass? c);
    }
}