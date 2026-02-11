using EduPortal.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduPortal.Domain.Entities
{
    public class Student : BaseEntity
    {
        public int UserId { get; set; }
        public AppUser User { get; set; } = null!;
        public string StudentNumber { get; set; } = string.Empty;
        public int? ClassId { get; set; }
        public Class? Class { get; set; }
        public int? ParentId { get; set; }
        public Parent? Parent { get; set; }

        public ICollection<StudentAssignment> StudentAssignments { get; set; } = new List<StudentAssignment>();
    }
}
