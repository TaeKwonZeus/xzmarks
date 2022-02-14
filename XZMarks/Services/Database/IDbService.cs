using System.Data.Common;

namespace XZMarks.Services.Database;

public interface IDbService
{
    DbConnection GetConnection();
}