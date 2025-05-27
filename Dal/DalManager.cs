using Dal.Api;
using Dal.Models;
using Dal.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class DalManager: IDal
    {
        dbcontext data;
        public ICustomers Customers { get; }

        public IFlight Flights { get; }

        public IClasses Classes { get; }

        public IClassToFlight ClassToFlight { get; }
        public IDestination Destination { get; }
        public IThisFlight ThisFlight { get; }
        public IOrder Order { get; }
        public IOrderDetails OrdersDetails { get; }



        public DalManager()
        {
            data = new dbcontext();
            Customers = new DalCustomersServices(data);
            Flights = new DalFlightsService(data);
            Classes = new DalClassService(data);
            ClassToFlight = new DalClassToFlightService(data);
            Destination = new DalDestinationService(data);
            ThisFlight = new DalThisFlightService(data);
            Order = new DalOrderService(data);
            OrdersDetails = new DalOrdersDetailService(data);

        }
    }
}
