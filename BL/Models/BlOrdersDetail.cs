using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class BlOrdersDetail
    {
        public int Id { get; set; }

        public int IdOrder { get; set; }

        public int IdClassToFlight { get; set; }

        public int CountTickets { get; set; }

        public int? CountOverLoad { get; set; }

        public int Price { get; set; }

        public virtual BlClassToFlight? IdClassToFlightNavigation { get; set; }

    }
}
