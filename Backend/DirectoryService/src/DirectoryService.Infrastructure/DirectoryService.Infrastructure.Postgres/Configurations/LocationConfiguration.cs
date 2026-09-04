using DirectoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("locations");

            builder.HasKey(l => l.Id).HasName("pk_locations");

            builder.Property(l => l.Id).HasColumnName("id");

            builder.Property(l => l.Name).IsRequired().HasMaxLength(100).HasColumnName("name");

            builder.Property(l => l.CreatedAt).HasColumnName("created_at").IsRequired();

            builder.Property(l => l.UpdatedAt).HasColumnName("updated_at").IsRequired();

            builder.ComplexProperty(l => l.Address, ab =>
            {
                ab.Property(a => a.Country).HasColumnName("country").IsRequired().HasMaxLength(100);
                ab.Property(a => a.City).HasColumnName("city").IsRequired().HasMaxLength(100);
                ab.Property(a => a.Street).HasColumnName("street").IsRequired().HasMaxLength(200);
                ab.Property(a => a.Office).HasColumnName("office").IsRequired().HasMaxLength(20);
            });
        }
    }
}
