namespace EduPortal.Application.DTOs;

public class TeacherNoteDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public int TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
