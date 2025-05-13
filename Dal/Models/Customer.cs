using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int IsManager { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
