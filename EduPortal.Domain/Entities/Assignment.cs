using EduPortal.Domain.Common;
using EduPortal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduPortal.Domain.Entities
{
    public class Assignment : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int QuestionCount { get; set; }
        public DateTime DueDate { get; set; }
        public AssignmentPriority Priority { get; set; } = AssignmentPriority.Medium;
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;
        public int TopicId { get; set; }
        public Topic Topic { get; set; } = null!;

        public ICollection<StudentAssignment> StudentAssignments { get; set; } = new List<StudentAssignment>();
    }
}
