using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DirectoryService.Infrastructure.Postgres.Repositories;

public class NpgsqlConnectionFactory : IDisposable, IAsyncDisposable
{
    private readonly NpgsqlDataSource _dataSource;

    public NpgsqlConnectionFactory(IConfiguration configuration)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("DirectoryServiceDb"));
        _dataSource = dataSourceBuilder.Build();
    }

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        return await _dataSource.OpenConnectionAsync();
    }

    public void Dispose()
    {
        _dataSource.Dispose();
    }


    public async ValueTask DisposeAsync()
    {
        await _dataSource.DisposeAsync();
    }

}