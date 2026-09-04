using DirectoryService.Domain.Entities;

namespace DirectoryService.Application;

public interface ILocationsRepository
{
    Task<Guid> AddAsync(Location location, CancellationToken cancellationToken);

    Task<Guid> UpdateAsync(Location location, CancellationToken cancellationToken);

    Task<Guid> DeleteAsync(Guid locationId, CancellationToken cancellationToken);

    Task<Location> GetByIdAsync(Guid locationId, CancellationToken cancellationToken);

    Task<Guid> GetByNameAsync(string name, CancellationToken cancellationToken);
}