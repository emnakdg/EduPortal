using EduPortal.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduPortal.Domain.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Color { get; set; }
        public int GradeLevel { get; set; }

        public ICollection<Unit> Units { get; set; } = new List<Unit>();
    }
}
