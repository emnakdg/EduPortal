using EduPortal.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduPortal.Domain.Entities
{
    public class Teacher : BaseEntity
    {
        public int UserId { get; set; }
        public AppUser User { get; set; } = null!;
        public string Branch { get; set; } = string.Empty;

        public ICollection<Class> Classes { get; set; } = new List<Class>();
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
        public ICollection<TeacherNote> TeacherNotes { get; set; } = new List<TeacherNote>();
    }
}
