using EduPortal.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduPortal.Domain.Entities
{
    public class Unit : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int OrderIndex { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;

        public ICollection<Topic> Topics { get; set; } = new List<Topic>();
    }
}
