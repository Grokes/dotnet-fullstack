using DirectoryService.Application;
using DirectoryService.Domain.Entities;

namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class LocationsRepository: ILocationsRepository
{
    public Task<Guid> AddAsync(Location location, CancellationToken cancellationToken)
    {
        return Task.FromResult(new Guid());
    }

    public Task<Guid> UpdateAsync(Location location, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> DeleteAsync(Guid locationId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Location> GetByIdAsync(Guid locationId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return Task.FromResult(new Guid());
    }
}