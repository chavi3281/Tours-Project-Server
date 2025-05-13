using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class ClassToFlight
{
    public int Id { get; set; }

    public int ClassId { get; set; }

    public int ThisflightId { get; set; }

    public int NumberOfSeats { get; set; }

    public int WeightLoad { get; set; }

    public int Hanacha { get; set; }

    public int Sold { get; set; }

    public int Price { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual ICollection<OrdersDetail> OrdersDetails { get; set; } = new List<OrdersDetail>();

    public virtual ThisFlight Thisflight { get; set; } = null!;
}
