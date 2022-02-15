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

        await connection.ExecuteAsync(@"INSERT INTO bookmarks (x, y, z, discord_id, name)
            VALUES (@X, @Y, @Z, @DiscordId, @Name);", new
        {
            coordinates.X,
            coordinates.Y,
            coordinates.Z,
            DiscordId = id,
            coordinates.Name
        });
    }

    public async Task<IEnumerable<Coordinates>> GetAllBookmarks(string id)
    {
        await using var connection = _dbService.GetConnection();

        var records = await connection.QueryAsync<BookmarkRecord>(@"SELECT x, y, z, name
            FROM bookmarks
            WHERE discord_id = @DiscordId;", new
        {
            DiscordId = id
        });

        return records.Select(r => new Coordinates(r.Name, r.X, r.Z, r.Y));
    }

    private record BookmarkRecord
    {
        public int X { get; set; }

        public int? Y { get; set; }

        public int Z { get; set; }

        public string DiscordId { get; set; }

        public string Name { get; set; }
    }
}