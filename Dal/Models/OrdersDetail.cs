using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class OrdersDetail
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int IdClassToFlight { get; set; }

    public int CountTickets { get; set; }

    public int? CountOverLoad { get; set; }

    public int Price { get; set; }

    public virtual ClassToFlight IdClassToFlightNavigation { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
