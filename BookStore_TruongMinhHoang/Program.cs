using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set your session timeout
    options.Cookie.HttpOnly = true; // Prevents client-side scripts from accessing the cookie
    options.Cookie.IsEssential = true; // Make the session cookie essential
});

// Configure authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Set your login path
        options.AccessDeniedPath = "/AccessDenied"; // Set your access denied path
        options.SlidingExpiration = true; // Reset the expiration time on each request
    });

// Configure authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin")); // Admin policy
});

// Other service registrations
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IBookRepo, BookRepo>();
builder.Services.AddScoped<IAuthorRepo, AuthorRepo>();
builder.Services.AddScoped<IPublisherRepo, PublisherRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession(); // Use session middleware before authentication and authorization
app.UseAuthentication(); // Authentication middleware
app.UseAuthorization(); // Authorization middleware

app.MapRazorPages();

app.Run();
