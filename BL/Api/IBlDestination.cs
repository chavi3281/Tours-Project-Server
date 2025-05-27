using BL.Models;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IBlDestination
    {
        Task<List<BlDestination>> GetAll();
        Task<List<BlDestination>> Create(BlDestination item);
        Task<BlDestination?> GetById(string destination);
        Task<List<BlDestination>> Update(BlDestination item);
        Task<BlDestination?> castOver(int d);
        Task Delete(int destination);
    }
}