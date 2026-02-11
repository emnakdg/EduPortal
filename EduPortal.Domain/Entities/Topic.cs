using EduPortal.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduPortal.Domain.Entities
{
    public class Topic : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int OrderIndex { get; set; }
        public int QuestionCount { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; } = null!;
    }
}
