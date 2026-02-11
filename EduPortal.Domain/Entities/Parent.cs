using EduPortal.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduPortal.Domain.Entities
{
    public class Parent : BaseEntity
    {
        public int UserId { get; set; }
        public AppUser User { get; set; } = null!;
        public string Phone { get; set; } = string.Empty;

        public ICollection<Student> Children { get; set; } = new List<Student>();
    }
}
