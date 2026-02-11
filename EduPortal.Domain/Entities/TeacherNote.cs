using EduPortal.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduPortal.Domain.Entities
{
    public class TeacherNote : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
    }
}
