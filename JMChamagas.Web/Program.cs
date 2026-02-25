using JMChamagas.Application;
using JMChamagas.Infrastructure;
using JMChamagas.Web.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/";
        options.AccessDeniedPath = "/";
        options.Cookie.Name = "JMChamagas.Auth";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
    });

builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapPost("/login", async (HttpContext httpContext, IConfiguration configuration) =>
{
    var form = await httpContext.Request.ReadFormAsync();
    var userName = form["username"].ToString();
    var password = form["password"].ToString();
    var returnUrl = form["returnUrl"].ToString();

    if (string.IsNullOrWhiteSpace(returnUrl) || !Uri.IsWellFormedUriString(returnUrl, UriKind.Relative))
        returnUrl = "/resumovendas";

    var expectedUser = configuration["AccessLogin:User"] ?? "admin";
    var expectedPassword = configuration["AccessLogin:Password"] ?? "admin";

    var userOk = string.Equals(userName?.Trim(), expectedUser, StringComparison.OrdinalIgnoreCase);
    var passwordOk = string.Equals(password, expectedPassword, StringComparison.Ordinal);

    if (!userOk || !passwordOk)
        return Results.Redirect("/?error=1", permanent: false);

    var claims = new List<Claim>
    {
        new(ClaimTypes.Name, expectedUser),
    };

    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var principal = new ClaimsPrincipal(identity);

    await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

    return Results.Redirect(returnUrl, permanent: false);
}).DisableAntiforgery();

app.MapPost("/logout", async (HttpContext httpContext) =>
{
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/", permanent: false);
}).DisableAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
