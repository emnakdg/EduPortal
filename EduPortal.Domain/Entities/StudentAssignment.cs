using EduPortal.Domain.Common;
using EduPortal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduPortal.Domain.Entities
{
    public class StudentAssignment : BaseEntity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; } = null!;
        public AssignmentStatus Status { get; set; } = AssignmentStatus.Pending;
        public int? Score { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public int EmptyAnswers { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string? TeacherFeedback { get; set; }
    }
}
