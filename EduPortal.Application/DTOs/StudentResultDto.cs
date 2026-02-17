using EduPortal.Domain.Enums;

namespace EduPortal.Application.DTOs;

public class StudentResultDto
{
    public int StudentAssignmentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string StudentNumber { get; set; } = string.Empty;
    public AssignmentStatus Status { get; set; }
    public int CorrectAnswers { get; set; }
    public int WrongAnswers { get; set; }
    public int EmptyAnswers { get; set; }
    public int? Score { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? TeacherFeedback { get; set; }
}