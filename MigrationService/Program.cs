
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserService.Infrastructure.DatabaseContext;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("MigrationService")));

var app = builder.Build();

using var scope = app.Services.CreateScope();

var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

db.Database.Migrate();

Console.WriteLine("Migration completed.");