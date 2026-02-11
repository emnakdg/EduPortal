namespace EduPortal.Application.DTOs;

public class SubjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Color { get; set; }
    public int GradeLevel { get; set; }
    public int UnitCount { get; set; }
    public int TopicCount { get; set; }
    public List<UnitDto> Units { get; set; } = new();
}

public class UnitDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<TopicDto> Topics { get; set; } = new();
}

public class TopicDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int QuestionCount { get; set; }
}