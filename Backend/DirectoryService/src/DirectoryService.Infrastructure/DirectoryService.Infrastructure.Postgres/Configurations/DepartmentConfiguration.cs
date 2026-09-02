using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DirectoryService.Domain.Entities;
using DirectoryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("departments");

            builder.HasKey(d => d.Id).HasName("pk_departments");

            builder.Property(d => d.Id).HasColumnName("id");

            builder.Property(d => d.Name).IsRequired().HasMaxLength(100).HasColumnName("name");

            builder.Property(d => d.CreatedAt).HasColumnName("created_at").IsRequired();

            builder.Property(d => d.UpdatedAt).HasColumnName("updated_at").IsRequired();

            builder.Property(d => d.Slug).HasColumnName("slug").HasConversion(s => s.Value, value => new Slug(value));
            
            builder.Property(d => d.Path).HasColumnName("path").IsRequired();

            builder.Property(d => d.ParentId).HasColumnName("parent_id").IsRequired(false);

            
        }
    }
}
