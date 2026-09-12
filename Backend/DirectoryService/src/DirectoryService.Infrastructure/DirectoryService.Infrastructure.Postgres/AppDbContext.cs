using DirectoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DirectoryService.Infrastructure.Postgres
{
    public class AppDbContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Location> Locations => Set<Location>(); 
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<DepartmentLocation> DepartmentLocations => Set<DepartmentLocation>();

        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly); 
        }
    }
}
