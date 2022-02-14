using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace XZMarks.Pages;

[Authorize]
public class Bookmarks : PageModel
{
    public void OnGet()
    {
        
    }
}