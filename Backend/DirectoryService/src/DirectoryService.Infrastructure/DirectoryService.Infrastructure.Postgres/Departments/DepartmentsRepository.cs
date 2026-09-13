using DirectoryService.Application.Departments;
using DirectoryService.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Infrastructure.Postgres.Departments;

public class DepartmentsRepository : IDepartmentsRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<DepartmentsRepository> _logger;

    public DepartmentsRepository(AppDbContext context, ILogger<DepartmentsRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Guid> AddAsync(Department department, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Departments.AddAsync(department, cancellationToken);
            
            await _context.SaveChangesAsync(cancellationToken);

            return department.Id;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка записи в БД");
            throw;
        }
    }

    public async Task<Department?> GetByIdAsync(Guid departmentId, CancellationToken cancellationToken)
    {
        var department = await _context
            .Departments
            .FindAsync([departmentId], cancellationToken);

        return department;
    }

    public async Task<Guid> AddWithLocationsAsync(
        Department department,
        IReadOnlyCollection<DepartmentLocation> departmentLocations,
        CancellationToken cancellationToken)
    {
        try
        {
            await _context.Departments.AddAsync(department, cancellationToken);

            await _context.DepartmentLocations.AddRangeAsync(departmentLocations, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return department.Id;
        }
        catch (Exception e)
        {
           _logger.LogError("Ошибка записи в БД");
           throw;
        }
       
    }
}