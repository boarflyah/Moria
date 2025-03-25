using MoriaWebAPIServices.Services.Interfaces.Orders;

namespace MoriaWebAPI.Services;

public class SubiektOrderUpdater : BackgroundService
{
    readonly IServiceScopeFactory _scopeFactory;
    readonly IConfiguration _configuration;
    readonly ILogger<SubiektOrderUpdater> _logger;

    public SubiektOrderUpdater(IServiceScopeFactory scopeFactory, IConfiguration configuration, ILogger<SubiektOrderUpdater> logger)
    {
        _scopeFactory = scopeFactory;
        _configuration = configuration;
        _logger = logger;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {

                using var scope = _scopeFactory.CreateAsyncScope();
                var orderService = scope.ServiceProvider.GetService<IOrderControllerService>();

                await orderService.ImportOrders();
                _logger.LogInformation($"Orders imported at: {DateTime.Now.ToString("g")}");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"{DateTime.Now.ToString("g")} - SubiektOrderUpdater");
            }
            finally
            {
                var updateFrequency = _configuration.GetValue<int>("UpdateFrequencyInMin", 60) * 60000;
                await Task.Delay(updateFrequency, stoppingToken);
            }
        }
    }
}
