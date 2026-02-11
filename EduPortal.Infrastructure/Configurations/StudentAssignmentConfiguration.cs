using EduPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduPortal.Infrastructure.Configurations
{
    public class StudentAssignmentConfiguration : IEntityTypeConfiguration<StudentAssignment>
    {
        public void Configure(EntityTypeBuilder<StudentAssignment> builder)
        {
            builder.HasKey(sa => sa.Id);
            builder.Property(sa => sa.TeacherFeedback).HasMaxLength(500);

            builder.HasOne(sa => sa.Student)
                   .WithMany(s => s.StudentAssignments)
                   .HasForeignKey(sa => sa.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sa => sa.Assignment)
                   .WithMany(a => a.StudentAssignments)
                   .HasForeignKey(sa => sa.AssignmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(sa => !sa.IsDeleted);
        }
    }
}
