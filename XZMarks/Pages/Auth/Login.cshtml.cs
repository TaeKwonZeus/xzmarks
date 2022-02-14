using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace XZMarks.Pages.Auth;

public class Login : PageModel
{
    public async Task OnGet()
    {
        await HttpContext.ChallengeAsync(new AuthenticationProperties
        {
            RedirectUri = "/"
        });
    }
}