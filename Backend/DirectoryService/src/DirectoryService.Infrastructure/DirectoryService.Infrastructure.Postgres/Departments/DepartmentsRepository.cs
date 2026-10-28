using DirectoryService.Application.Departments;
using DirectoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Department>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Departments.ToListAsync(cancellationToken);
    }

    public async Task<Guid> DeleteAsync(Guid departmentId, CancellationToken cancellationToken)
    {
        await _context.Departments.Where(x => x.Id == departmentId).ExecuteDeleteAsync(cancellationToken);
        return departmentId;
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Guid> AddLocationAsync(Guid departmentId, Guid locationId, CancellationToken cancellationToken)
    {
        var departmentLocation = new DepartmentLocation(departmentId, locationId);

        await _context.DepartmentLocations.AddAsync(departmentLocation, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return departmentLocation.Id;
    }


    public async Task<List<Guid>> GetLocationsAsync(Guid departmentId, CancellationToken cancellationToken)
    {
        return await _context.DepartmentLocations.Where(x => x.DepartmentId == departmentId)
            .Select(x => x.LocationId).ToListAsync(cancellationToken);
    }

    public async Task<Guid> DeleteLocationAsync(Guid departmentId, Guid locationId, CancellationToken cancellationToken)
    {
        await _context.DepartmentLocations.Where(x => x.DepartmentId == departmentId && x.LocationId == locationId)
            .ExecuteDeleteAsync(cancellationToken);
        return departmentId;
    }
}