using EduPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduPortal.Infrastructure.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.StudentNumber).IsRequired().HasMaxLength(20);

            builder.HasOne(s => s.User)
                   .WithOne()
                   .HasForeignKey<Student>(s => s.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Class)
                   .WithMany(c => c.Students)
                   .HasForeignKey(s => s.ClassId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(s => s.Parent)
                   .WithMany(p => p.Children)
                   .HasForeignKey(s => s.ParentId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasQueryFilter(s => !s.IsDeleted);
        }
    }
}
