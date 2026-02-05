using System;
using System.Collections.Generic;

namespace Individuellt_databasproject_V2.Models;

public partial class Staff
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? Lastname { get; set; }

    public string? SocialSecurityNumber { get; set; }

    public int PositionId { get; set; }

    public int DepartmentId { get; set; }

    public decimal Salary { get; set; }

    public DateOnly HireDate { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual Position Position { get; set; } = null!;
}
