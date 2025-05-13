using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dal.Api
{
    public interface IDal
    {
        public ICustomers Customers { get; }
        public IFlight Flights { get; }
        public IClasses Classes { get; }
        public IClassToFlight ClassToFlight { get; }
        public IDestination Destination { get; }
        public IThisFlight ThisFlight { get; }
        public IOrder Order { get; }
        public IOrderDetails OrdersDetails { get; }

    }
}
