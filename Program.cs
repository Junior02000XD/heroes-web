using Microsoft.EntityFrameworkCore;
using HeroesWeb.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<HeroesContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("HeroesDb")
        ?? throw new InvalidOperationException("Falta la conexión HeroesDb.")));
}
else if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<HeroesContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("HeroesDb")
        ?? throw new InvalidOperationException("Falta la conexión HeroesDb.")));
}


builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 3;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
}).AddEntityFrameworkStores<HeroesContext>();


builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeAreaFolder("Heroes", "/");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();
app.Run();
