using DirectoryService.Domain.Entities;

namespace DirectoryService.Application.Departments;

public interface IDepartmentsRepository
{
    public Task<Guid> AddAsync(Department department, CancellationToken cancellationToken);
    public Task<Department?> GetByIdAsync(Guid departmetnId, CancellationToken cancellationToken);

    public Task<Guid> AddWithLocationsAsync(Department department,
        IReadOnlyCollection<DepartmentLocation> departmentLocations,
        CancellationToken cancellationToken);

    public Task<List<Department>> GetAllAsync(CancellationToken cancellationToken);
    public Task<Guid> DeleteAsync(Guid departmentId, CancellationToken cancellationToken);
    public Task<int> SaveAsync(CancellationToken cancellationToken);
    public Task<Guid> AddLocationAsync(Guid departmentId, Guid locationId, CancellationToken cancellationToken);
    public Task<List<Guid>> GetLocationsAsync(Guid departmentId, CancellationToken cancellationToken);
    public Task<Guid> DeleteLocationAsync(Guid departmentId, Guid locationId, CancellationToken cancellationToken);
}