using EduPortal.Domain.Enums;

namespace EduPortal.Application.DTOs;

public class AssignmentDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string TopicName { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public int QuestionCount { get; set; }
    public DateTime DueDate { get; set; }
    public AssignmentPriority Priority { get; set; }


    public int TotalStudents { get; set; }
    public int CompletedStudents { get; set; }
    public double AverageScore { get; set; }


    public List<StudentResultDto> StudentResults { get; set; } = new();
}