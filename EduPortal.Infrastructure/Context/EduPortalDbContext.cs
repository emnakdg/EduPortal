using EduPortal.Application.Common.Interfaces;
using EduPortal.Domain.Common;
using EduPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduPortal.Infrastructure.Context
{
    public class EduPortalDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>, IEduPortalDbContext
    {
        public EduPortalDbContext(DbContextOptions<EduPortalDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Parent> Parents => Set<Parent>();
        public DbSet<Class> Classes => Set<Class>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<Unit> Units => Set<Unit>();
        public DbSet<Topic> Topics => Set<Topic>();
        public DbSet<Assignment> Assignments => Set<Assignment>();
        public DbSet<StudentAssignment> StudentAssignments => Set<StudentAssignment>();
        public DbSet<TeacherNote> TeacherNotes => Set<TeacherNote>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(EduPortalDbContext).Assembly);

            builder.Entity<AppUser>().ToTable("Users");
            builder.Entity<IdentityRole<int>>().ToTable("Roles");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Modified)
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
