using EduPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduPortal.Infrastructure.Configurations
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired().HasMaxLength(200);

            builder.HasOne(t => t.Unit)
                   .WithMany(u => u.Topics)
                   .HasForeignKey(t => t.UnitId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(t => !t.IsDeleted);
        }
    }
}
