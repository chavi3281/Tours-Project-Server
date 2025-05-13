using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class BlClassToFlight
    {
        public int Id { get; set; }

        public int ClassId { get; set; }

        public int ThisflightId { get; set; }

        public int NumberOfSeats { get; set; }

        public int Price { get; set; }

        public int WeightLoad { get; set; }

        public int Hanacha { get; set; }
        public int Sold { get; set; }

        public virtual BlClass? Class { get; set; }

        public virtual BlThisFlight? Thisflight { get; set; }

    }
}
