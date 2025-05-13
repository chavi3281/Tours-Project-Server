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
        Destination? GetById(string destination);
        List<Destination> GetAll();
        List<Destination> Create(Destination item);
        Destination? Update(Destination item);
        void Delete(int destination);
    }
}
