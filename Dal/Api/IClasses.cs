using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IClasses
    {
        List<Class> GetAll();
        Class? GetById(int description);
        void Create(Class item);
        Class Update(Class item);
        void Delete(string description);

    }
}
