using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class Flight
{
    public int Id { get; set; }

    public int Source { get; set; }

    public int Destination { get; set; }

    public int TimeOfFlight { get; set; }

    public int Sold { get; set; }

    public virtual Destination DestinationNavigation { get; set; } = null!;

    public virtual Destination SourceNavigation { get; set; } = null!;

    public virtual ICollection<ThisFlight> ThisFlights { get; set; } = new List<ThisFlight>();
}
