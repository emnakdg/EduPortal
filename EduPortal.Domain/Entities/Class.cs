using EduPortal.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduPortal.Domain.Entities
{
    public class Class : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public string? Room { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;

        public ICollection<Student> Students { get; set; } = new List<Student>();

    }
}
