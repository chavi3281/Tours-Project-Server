using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class BlDestination
    {
        public int Id { get; set; }

        public string Destination { get; set; } = null!;

        public string? Path { get; set; }



    }
}
