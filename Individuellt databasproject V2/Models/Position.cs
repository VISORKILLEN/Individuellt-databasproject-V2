using System;
using System.Collections.Generic;

namespace Individuellt_databasproject_V2.Models;

public partial class Position
{
    public int Id { get; set; }

    public string PositionName { get; set; } = null!;

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
