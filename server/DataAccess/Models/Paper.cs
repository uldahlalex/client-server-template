using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Paper
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Discontinued { get; set; }

    public int Stock { get; set; }

    public double Price { get; set; }

    public virtual ICollection<OrderEntry> OrderEntries { get; set; } = new List<OrderEntry>();

    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
}
