using DirectoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.ToTable("positions");

            builder.HasKey(p => p.Id).HasName("pk_positions");
            
            builder.Property(p => p.Id).HasColumnName("id");

            builder.Property(p => p.Name).IsRequired().HasMaxLength(100).HasColumnName("name");

            builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();

            builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();
        }
    }
}
