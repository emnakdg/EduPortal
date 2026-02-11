namespace EduPortal.Application.DTOs;

public class ClassDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Semester { get; set; } = string.Empty;
    public string? Room { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    public int StudentCount { get; set; }
}