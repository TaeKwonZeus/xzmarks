using AspNet.Security.OAuth.Discord;
using Microsoft.AspNetCore.Authentication.Cookies;
using XZMarks.Services.Bookmarks;
using XZMarks.Services.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = DiscordAuthenticationDefaults.AuthenticationScheme;
    })
    .AddDiscord(options =>
    {
        var authenticationSettings = builder.Configuration.GetSection("Authentication");
        options.ClientId = authenticationSettings["ClientId"];
        options.ClientSecret = authenticationSettings["ClientSecret"];
    })
    .AddCookie();

builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddScoped<IBookmarkService, BookmarkService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();