using CurrencyUpdater;
using CurrencyUpdater.Services;
using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.DatabaseContext;
using System.Text;

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration
        .GetConnectionString("DefaultConnection"));
});


builder.Services.AddHttpClient<CbrCurrencyService>();


builder.Services.AddHostedService<Worker>();


var host = builder.Build();

host.Run();