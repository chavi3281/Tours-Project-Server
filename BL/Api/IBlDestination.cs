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
        List<BlDestination> GetAll();
        List<BlDestination> Create(BlDestination item);
        BlDestination? GetById(string destination);

        /*        public void Create(List<BlDestination> item);
        */
        List<BlDestination> Update(BlDestination item);
        public BlDestination castOver(int d);
        void Delete(int destination);
    }
}
