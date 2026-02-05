using System;
using System.Collections.Generic;

namespace Individuellt_databasproject_V2.Models;

public partial class Subject
{
    public int Id { get; set; }

    public string? SubjectName { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
