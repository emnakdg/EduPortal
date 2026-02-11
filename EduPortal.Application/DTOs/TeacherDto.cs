namespace EduPortal.Application.DTOs;

public class TeacherDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Branch { get; set; } = string.Empty;
    public int ClassCount { get; set; }
    public int StudentCount { get; set; }
    public int UserId { get; set; }
}