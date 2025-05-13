using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class BlOrder
    {



        public int Id { get; set; }

        public int IdCustomer { get; set; }

        public int Price { get; set; }

        public DateOnly Date { get; set; }

        public virtual BlCustomers? IdCustomerNavigation { get; set; }
        public virtual ICollection<BlOrdersDetail> OrdersDetails { get; set; } = new List<BlOrdersDetail>();

    }
}
