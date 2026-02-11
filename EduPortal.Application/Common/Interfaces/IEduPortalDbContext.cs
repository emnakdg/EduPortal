using EduPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Application.Common.Interfaces;

public interface IEduPortalDbContext
{
    DbSet<Student> Students { get; }
    DbSet<Teacher> Teachers { get; }
    DbSet<Parent> Parents { get; }
    DbSet<Class> Classes { get; }
    DbSet<Subject> Subjects { get; }
    DbSet<Unit> Units { get; }
    DbSet<Topic> Topics { get; }
    DbSet<Assignment> Assignments { get; }
    DbSet<StudentAssignment> StudentAssignments { get; }
    DbSet<TeacherNote> TeacherNotes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}