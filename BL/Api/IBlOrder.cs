using BL.Models;
using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IBlOrder
    {
        BlOrder? GetById(int id);
        List<BlOrder>? GetByCustomerId(int id);

        List<BlOrder> GetAll();
        void Create(BlOrder item);
        void Delete(int id);
        List<BlOrder>? GetByClassToFlightId(int id);
    }

}
