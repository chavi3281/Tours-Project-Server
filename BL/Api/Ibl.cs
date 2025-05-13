using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface Ibl
    {
        public IBlCustomers Customers { get; }
        public IBlFlight Flights { get; }
        public IBlClassToFlight ClassToFlight { get; }
        public IBlDestination Destination { get; }
        public IBlClass Classes { get;}
        public IBlThisFlight ThisFlight { get; }
        public IBlOrder Order { get; }

        public IBlOrdersDetail OrderDetails { get; }
    }
}
