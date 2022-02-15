using XZMarks.Models;

namespace XZMarks.Services.Bookmarks;

public interface IBookmarkService
{
    Task CreateBookmark(string id, Coordinates coordinates);

    Task<IEnumerable<Coordinates>> GetAllBookmarks(string id);
}