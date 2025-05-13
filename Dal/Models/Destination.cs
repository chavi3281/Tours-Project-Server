using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class Destination
{
    public int Id { get; set; }

    public string Destination1 { get; set; } = null!;

    public string? Path { get; set; }

    public virtual ICollection<Flight> FlightDestinationNavigations { get; set; } = new List<Flight>();

    public virtual ICollection<Flight> FlightSourceNavigations { get; set; } = new List<Flight>();
}
