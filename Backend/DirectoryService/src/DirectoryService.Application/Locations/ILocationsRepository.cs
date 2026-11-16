using DirectoryService.Domain.Entities;

namespace DirectoryService.Application.Locations;

public interface ILocationsRepository
{
    Task<Guid> AddAsync(Location location, CancellationToken cancellationToken);

    Task<Guid> UpdateAsync(Location location, CancellationToken cancellationToken);

    Task<Guid> DeleteAsync(Guid locationId, CancellationToken cancellationToken);

    Task<Location?> GetByIdAsync(Guid locationId, CancellationToken cancellationToken);

    Task<Guid> GetIdByNameAsync(string name, CancellationToken cancellationToken);

    Task<bool> IsAllExistAsync(IReadOnlyCollection<Guid> locationIds, CancellationToken cancellationToken);

    Task<List<Location>> GetAllAsync(CancellationToken cancellationToken);

    Task<int> SaveAsync(CancellationToken cancellationToken);
}