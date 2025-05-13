using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class BlFlights
    {

        public int Id { get; set; }

        public int Source { get; set; }

        public int Destination { get; set; }

        public int TimeOfFlight { get; set; }

        public int Sold { get; set; }

        public virtual BlDestination? DestinationNavigation { get; set; }

        public virtual BlDestination? SourceNavigation { get; set; }

    }

}

