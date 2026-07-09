using CurrencyUpdater.Services;
using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.DatabaseContext;

namespace CurrencyUpdater;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly int _intervalHours;

    public Worker(
    ILogger<Worker> logger,
    IServiceProvider serviceProvider,
    IConfiguration configuration)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;

        _intervalHours = configuration
            .GetValue<int>("CurrencyUpdate:IntervalHours");

        if (_intervalHours <= 0)
        {
            throw new InvalidOperationException(
                "Configuration value 'CurrencyUpdate:IntervalHours' must be greater than zero.");
        }
    }


    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();


                var cbrService = scope
                    .ServiceProvider
                    .GetRequiredService<CbrCurrencyService>();


                var dbContext = scope
                    .ServiceProvider
                    .GetRequiredService<AppDbContext>();


                var currencies = await cbrService
                    .GetCurrenciesAsync();


                await dbContext.Currencies
                    .ExecuteDeleteAsync(
                        stoppingToken);


                await dbContext.Currencies
                    .AddRangeAsync(
                        currencies,
                        stoppingToken);


                await dbContext.SaveChangesAsync(
                    stoppingToken);


                _logger.LogInformation(
                    "Currencies updated. Count: {count}",
                    currencies.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error while updating currencies");
            }


            await Task.Delay(
                    TimeSpan.FromHours(_intervalHours),
                    stoppingToken);
        }
    }
}
