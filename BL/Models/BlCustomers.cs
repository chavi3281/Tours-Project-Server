
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class BlCustomers
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string LastName { get; set; } = null!;

        public string? Phone { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
        public int IsManager { get; set; }
    }
}
