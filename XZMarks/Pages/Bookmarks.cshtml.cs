using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XZMarks.Models;
using XZMarks.Services.Bookmarks;

namespace XZMarks.Pages;

[Authorize]
public class Bookmarks : PageModel
{
    private readonly IBookmarkService _bookmarkService;
    private readonly ILogger<Bookmarks> _logger;

    public Bookmarks(IBookmarkService bookmarkService, ILogger<Bookmarks> logger)
    {
        _bookmarkService = bookmarkService;
        _logger = logger;
    }

    [BindProperty]
    public Bookmark FormValue { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var coordinates = new Coordinates(FormValue.Name, FormValue.X, FormValue.Z, FormValue.Y);
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        _logger.LogInformation("{Name}: {X} {Y} {Z}", FormValue.Name, FormValue.X, FormValue.Y, FormValue.Z);

        try
        {
            await _bookmarkService.CreateBookmark(id, coordinates);

            return Page();
        }
        catch (DbException e)
        {
            _logger.LogWarning(e, "Something went horribly wrong: {Error}", e.Message);

            return Redirect("/");
        }
    }

    public class Bookmark
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public int X { get; set; }
        
        public int? Y { get; set; }
        
        [Required]
        public int Z { get; set; }
    }
}