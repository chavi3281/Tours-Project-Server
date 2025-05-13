using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class Order
{
    public int Id { get; set; }

    public int IdCustomer { get; set; }

    public DateTime Date { get; set; }

    public int Price { get; set; }

    public virtual Customer IdCustomerNavigation { get; set; } = null!;

    public virtual ICollection<OrdersDetail> OrdersDetails { get; set; } = new List<OrdersDetail>();
}
