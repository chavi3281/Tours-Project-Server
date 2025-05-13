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
        List<BlClass> GetAll();
        BlClass? GetById(int id);
        List<BlClass> Create(BlClass item);
        List<BlClass> Update(BlClass item);
        List<BlClass> Delete(string description);
        public BlClass castingClassFromDalToBl(Class c);
        public Class castingClassFromBlToDal(BlClass c);
    }
}
