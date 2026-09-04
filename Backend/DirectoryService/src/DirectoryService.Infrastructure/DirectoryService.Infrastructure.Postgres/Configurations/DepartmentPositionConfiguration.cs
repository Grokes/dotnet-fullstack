using DirectoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DirectoryService.Infrastructure.Postgres.Configurations
{
    public class DepartmentPositionConfiguration : IEntityTypeConfiguration<DepartmentPosition>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DepartmentPosition> builder)
        {
            builder.ToTable("department_positions");

                builder.HasKey(dl => dl.Id).HasName("pk_department_positions");

                builder.Property(dp => dp.Id).IsRequired().HasColumnName("id");

                builder.Property(dp => dp.DepartmentId).IsRequired().HasColumnName("department_id");

                builder.Property(dp => dp.PositionId).IsRequired().HasColumnName("position_id");

                builder.HasOne<Department>().WithMany().HasForeignKey(dl => dl.DepartmentId).IsRequired().OnDelete(DeleteBehavior.Cascade);

                builder.HasOne<Position>().WithMany().HasForeignKey(dl => dl.PositionId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}