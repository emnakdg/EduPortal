using EduPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduPortal.Infrastructure.Configurations
{
    public class TeacherNoteConfiguration : IEntityTypeConfiguration<TeacherNote>
    {
        public void Configure(EntityTypeBuilder<TeacherNote> builder)
        {
            builder.HasKey(tn => tn.Id);
            builder.Property(tn => tn.Content).IsRequired().HasMaxLength(1000);

            builder.HasOne(tn => tn.Teacher)
                   .WithMany(t => t.TeacherNotes)
                   .HasForeignKey(tn => tn.TeacherId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(tn => tn.Student)
                   .WithOne()
                   .HasForeignKey<TeacherNote>(tn => tn.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(tn => !tn.IsDeleted);
        }
    }
}
