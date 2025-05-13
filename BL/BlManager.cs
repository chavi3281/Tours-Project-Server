using BL.Api;
using BL.Models;
using BL.Services;
using Dal;
using Dal.Api;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BlManager : Ibl
    {
        public IBlCustomers Customers { get; set; }
        public IBlFlight Flights { get; set; }
        public IBlClassToFlight ClassToFlight { get; set; }
        public IBlClass Classes { get; set; }
        public IBlDestination Destination { get; set; }
        public IBlThisFlight ThisFlight { get; set; }
        public IBlOrder Order { get; set; }
        public IBlOrdersDetail OrderDetails { get; set; }


        public BlManager() 
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<IDal, DalManager>();
            services.AddSingleton<IBlCustomers, BlCustomersService>();
            services.AddSingleton<IBlFlight, BlFlightService>();
            services.AddSingleton<IBlClassToFlight, BlClassToFlightService>();
            services.AddSingleton<IBlDestination, BlDestinationService>();
            services.AddSingleton<IBlClass, BlClasssService>();
            services.AddSingleton<IBlThisFlight,BlThisFlightService>();
            services.AddSingleton<IBlOrder, BlOrderService>();
            services.AddSingleton<IBlOrdersDetail, BlOrdersDetailService>();


            ServiceProvider servicesProvider = services.BuildServiceProvider();

            Customers = servicesProvider.GetRequiredService<IBlCustomers>();

            Flights = servicesProvider.GetRequiredService<IBlFlight>();

            ClassToFlight = servicesProvider.GetRequiredService<IBlClassToFlight>();

            Classes = servicesProvider.GetRequiredService<IBlClass>();


            Destination = servicesProvider.GetRequiredService<IBlDestination>();

            ThisFlight = servicesProvider.GetRequiredService<IBlThisFlight>();

            Order = servicesProvider.GetRequiredService<IBlOrder>();

            OrderDetails = servicesProvider.GetRequiredService<IBlOrdersDetail>();
        }

    }
}
