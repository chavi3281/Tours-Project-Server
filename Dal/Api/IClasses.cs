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
        Task<List<Class>> GetAll();
        Task<Class?> GetById(int description);
        Task Create(Class item);
        Task<List<Class>> Update(Class item);
        Task Delete(string description);

    }
}
