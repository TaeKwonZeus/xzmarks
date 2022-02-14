using System.Data.Common;
using Microsoft.Data.Sqlite;

namespace XZMarks.Services.Database;

public class DbService : IDbService
{
    private readonly string _connectionString;

    public DbService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("App");
    }

    public DbConnection GetConnection()
    {
        return new SqliteConnection(_connectionString);
    }
}