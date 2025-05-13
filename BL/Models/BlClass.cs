using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class BlClass
    {
        public int Id { get; set; }

        public string Description { get; set; } = null!;

/*        public virtual ICollection<ClassToFlight> ClassToFlights { get; set; } = new List<ClassToFlight>();*/
    }
}
