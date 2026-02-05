using System;
using System.Collections.Generic;

namespace Individuellt_databasproject_V2.Models;

public partial class Class
{
    public int Id { get; set; }

    public string? ClassName { get; set; }

    public int? MentorId { get; set; }

    public virtual Staff? Mentor { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
