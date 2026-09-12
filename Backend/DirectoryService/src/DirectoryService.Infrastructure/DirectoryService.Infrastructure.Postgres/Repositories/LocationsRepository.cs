using DirectoryService.Application.Locations;
using DirectoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class LocationsRepository : ILocationsRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<LocationsRepository> _logger;

    public LocationsRepository(AppDbContext context, ILogger<LocationsRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Guid> AddAsync(Location location, CancellationToken cancellationToken)
    {
        try
        {
            await _context.Locations.AddAsync(location, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return location.Id;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка записи в БД");
            throw;
        }
    }

    public async Task<Guid> GetIdByNameAsync(string name, CancellationToken cancellationToken)
    {
        var locationId = await _context.Locations
            .Where(x => x.Name == name)
            .Select(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);

        return locationId;
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


    public async Task<bool> IsAllExistAsync(IReadOnlyCollection<Guid> locationIds, CancellationToken cancellationToken)
    {
        var count = await _context.Locations.CountAsync(x => locationIds.Contains(x.Id), cancellationToken);

        return count == locationIds.Count;
    }
}