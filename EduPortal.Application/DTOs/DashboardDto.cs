namespace EduPortal.Application.DTOs;

public class AdminDashboardDto
{
    public int TotalStudents { get; set; }
    public int TotalTeachers { get; set; }
    public int TotalClasses { get; set; }
    public int PendingAssignments { get; set; }
    public List<StudentDto> RecentStudents { get; set; } = new();
}

public class TeacherDashboardDto
{
    public string TeacherName { get; set; } = string.Empty;
    public List<ClassDto> Classes { get; set; } = new();
    public List<AssignmentDto> RecentAssignments { get; set; } = new();
    public int TotalStudents { get; set; }
    public int TotalClasses { get; set; }
    public int ActiveAssignmentCount { get; set; }
    public double OverallSuccessRate { get; set; }
}

public class StudentDashboardDto
{
    public string StudentName { get; set; } = string.Empty;
    public int PendingCount { get; set; }
    public int CompletedCount { get; set; }
    public int TotalQuestionsSolved { get; set; }
    public int Streak { get; set; }
    public List<AssignmentDto> ActiveAssignments { get; set; } = new();
}