using Dapper;
using XZMarks.Models;
using XZMarks.Services.Database;

namespace XZMarks.Services.Bookmarks;

public class BookmarkService : IBookmarkService
{
    private readonly IDbService _dbService;

    public BookmarkService(IDbService dbService)
    {
        _dbService = dbService;
    }

    public async Task CreateBookmark(string id, Coordinates coordinates)
    {
        await using var connection = _dbService.GetConnection();

        await connection.ExecuteAsync(@"INSERT INTO bookmarks (x, y, z, discord_id, name, description)
            VALUES (@X, @Y, @Z, @DiscordId, @Name, @Description);", new
        {
            coordinates.X,
            coordinates.Y,
            coordinates.Z,
            DiscordId = id,
            coordinates.Name,
            coordinates.Description
        });
    }
}