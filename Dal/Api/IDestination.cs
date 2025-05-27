using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IDestination
    {
        Task<Destination?> GetById(string destination);
        Task<List<Destination>> GetAll();
        Task<List<Destination>> Create(Destination item);
        Task<Destination?> Update(Destination item);
        Task Delete(int destination);
    }
}
