using Dapper;
using DirectoryService.Application;
using DirectoryService.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class NpgsqlLocationsRepository : ILocationsRepository
{
    private readonly NpgsqlConnectionFactory _connectionFactory;
    private readonly ILogger<NpgsqlLocationsRepository> _logger;

    public NpgsqlLocationsRepository(NpgsqlConnectionFactory connectionFactory,
        ILogger<NpgsqlLocationsRepository> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    public async Task<Guid> AddAsync(Location location, CancellationToken cancellationToken)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        using var transaction = connection.BeginTransaction();

        var sqlCommand = """
                         INSERT INTO locations (id, name, created_at, updated_at, city, country, office, street)
                         VALUES (@Id, @Name, @CreatedAt, @UpdatedAt, @City, @Country, @Office, @Street)
                         """;

        var sqlParams = new
        {
            Id = location.Id,
            Name = location.Name,
            CreatedAt = location.CreatedAt,
            UpdatedAt = location.UpdatedAt,
            City = location.Address.City,
            Country = location.Address.Country,
            Office = location.Address.Office,
            Street = location.Address.Street,
        };

        try
        {
            await connection.ExecuteAsync(
                new CommandDefinition(
                    sqlCommand,
                    sqlParams,
                    transaction,
                    cancellationToken: cancellationToken));
            transaction.Commit();
            return location.Id;
        }
        catch (Exception e)
        {
            _logger.LogInformation("Ошибка записи в БД");
            transaction.Rollback();
            throw;
        }
    }

    public async Task<Guid> GetIdByNameAsync(string name, CancellationToken cancellationToken)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();


        var sqlCommand = """
                         SELECT id FROM locations
                         WHERE name = @Name
                         """;

        var sqlParams = new
        {
            Name = name,
        };

        var locationId = await connection.QuerySingleOrDefaultAsync<Guid>(
            new CommandDefinition(
                sqlCommand,
                sqlParams,
                cancellationToken: cancellationToken));
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