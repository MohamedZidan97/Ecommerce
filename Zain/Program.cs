using Microsoft.AspNetCore.Identity;
using Zain.Domain.Entities;
using Zain.Persistance;
using Zain.Application;
using Zain.Persistance.SeedData.Roles;
using Zain.Persistance.SeedData.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Application Container
builder.Services.AddApplicationService();

// Persistance Container

builder.Services.AddPersistenceService(builder.Configuration);

// DI Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
             options =>
             {
                 // Default Password settings.
                 options.Password.RequireDigit = false;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = false;
                 options.Password.RequiredLength = 3;
                 options.Password.RequiredUniqueChars = 0;
             }
             )
             .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders();



var app = builder.Build();

// seed Roles
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RolesSeeder.SeedAsync(services);
}

// seed user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await UserSeeder.SeedAsync(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
