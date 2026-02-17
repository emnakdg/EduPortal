using EduPortal.Domain.Enums;

namespace EduPortal.Application.DTOs;

public class StudentAssignmentDto
{
    public int Id { get; set; }
    public int AssignmentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string TopicName { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public string TeacherName { get; set; } = string.Empty;
    public int QuestionCount { get; set; }
    public DateTime DueDate { get; set; }
    public AssignmentPriority Priority { get; set; }
    public AssignmentStatus Status { get; set; }
    public int? Score { get; set; }
    public int CorrectAnswers { get; set; }
    public int WrongAnswers { get; set; }
    public int EmptyAnswers { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? TeacherFeedback { get; set; }
}
