using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class BlThisFlight
    {
        public int Id { get; set; }

        public int FlightId { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public decimal PriceToOverLoad { get; set; }

        public virtual BlFlights? Flight { get; set; }



    }
}
