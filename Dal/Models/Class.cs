using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class Class
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<ClassToFlight> ClassToFlights { get; set; } = new List<ClassToFlight>();
}
