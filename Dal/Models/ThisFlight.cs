using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class ThisFlight
{
    public int Id { get; set; }

    public int FlightId { get; set; }

    public DateTime Date { get; set; }

    public decimal PriceToOverLoad { get; set; }

    public virtual ICollection<ClassToFlight> ClassToFlights { get; set; } = new List<ClassToFlight>();

    public virtual Flight Flight { get; set; } = null!;
}
