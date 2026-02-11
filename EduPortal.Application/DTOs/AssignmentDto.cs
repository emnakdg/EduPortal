using EduPortal.Domain.Enums;

namespace EduPortal.Application.DTOs;

public class AssignmentDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string TopicName { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public string TeacherName { get; set; } = string.Empty;
    public int QuestionCount { get; set; }
    public DateTime DueDate { get; set; }
    public AssignmentPriority Priority { get; set; }
    public int AssignedStudentCount { get; set; }
}