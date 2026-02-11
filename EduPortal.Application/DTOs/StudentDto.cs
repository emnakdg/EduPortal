namespace EduPortal.Application.DTOs;

public class StudentDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string StudentNumber { get; set; } = string.Empty;
    public string? ClassName { get; set; }
    public int CompletedAssignments { get; set; }
    public int PendingAssignments { get; set; }
    public double SuccessRate { get; set; }
}