using DirectoryService.Application;
using DirectoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class EfLocationsRepository : ILocationsRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<EfLocationsRepository> _logger;

    public EfLocationsRepository(AppDbContext context, ILogger<EfLocationsRepository> logger)
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
            _logger.LogInformation("Ошибка записи в БД");
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
}