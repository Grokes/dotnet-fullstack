using DirectoryService.Domain.Entities;

namespace DirectoryService.Application.Departments;

public interface IDepartmentsRepository
{
    public Task<Guid> AddAsync(Department department, CancellationToken cancellationToken);
    public Task<Department?> GetByIdAsync(Guid departmetnId, CancellationToken cancellationToken);

    public Task<Guid> AddWithLocationsAsync(Department department,
        IReadOnlyCollection<DepartmentLocation> departmentLocations,
        CancellationToken cancellationToken);
}