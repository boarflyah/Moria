using MoriaWebAPIServices.Services.Interfaces.Orders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoriaWebAPIServices.Services.Orders;
using Microsoft.Extensions.Configuration;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services.Subiekt.Core;
using MoriaWebAPIServices.Services;
using Serilog;
using Serilog.Events;
using Microsoft.EntityFrameworkCore;
using MoriaWebAPIServices.Services.Subiekt.Interfaces;
using MoriaWebAPIServices.Services.Subiekt.Services;
using MoriaWebAPIServices.Services.Dictionaries;
using MoriaWebAPIServices.Services.DriveComponents;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;
using MoriaWebAPIServices.Services.Interfaces.DriveComponents;
using MoriaWebAPIServices.Services.Interfaces.Products;
using MoriaWebAPIServices.Services.Interfaces;
using MoriaWebAPIServices.Services.Products;

internal class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                //.UseSerilog((context, services, loggerConfig) =>
                //{
                //    loggerConfig
                //        .WriteTo.Console()
                //        .WriteTo.File("C:\\Apps\\Logs\\ImportConsole\\log-.txt", rollingInterval: RollingInterval.Day);
                //})
                .ConfigureServices((context, services) =>
                {
                    IConfiguration config = context.Configuration;


                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseNpgsql(config.GetConnectionString("MoriaDatabase")));

                    services.AddSingleton<ModelTypeConverter>();
                    services.AddScoped<ModelsCreator>();
                    services.AddSingleton<LockService>();

                    services.Configure<CredentialsConfig>(
                        config.GetSection("Credentials"));
                    services.AddScoped<ICredentialsService, CoreCredentialsService>();
                    services.AddScoped<IDictionariesService, DictionariesService>();
                    services.AddScoped<IMoriaHandlerService, MoriaHandlerService>();
                    services.AddScoped<ISalesOrderQueriesService, TestSalesOrderQueriesService>();
                    services.AddScoped<ISalesOrderService, SalesOrderService>();

                    services.AddScoped<IOrderControllerService, OrderControllerService>();
                    services.AddScoped<IEmployeeControllerService, EmployeeControllerService>();
                    services.AddScoped<IWarehouseControllerService, WarehouseControllerService>();
                    services.AddScoped<IColorControllerService, ColorControllerService>();
                    services.AddScoped<IContactControllerService, ContactControllerService>();
                    services.AddScoped<IMotorControllerService, MotorControllerService>();
                    services.AddScoped<IMotorGearControllerService, MotorGearControllerService>();
                    services.AddScoped<IPositionControllerService, PositionControllerService>();
                    services.AddScoped<ISteelKindControllerService, SteelKindControllerService>();
                    services.AddScoped<IProductControllerService, ProductControllerService>();
                    services.AddScoped<IElectricalCabinetControllerService, ElectricalCabinetControllerService>();
                    services.AddScoped<ICategoryControllerService, CategoryControllerService>();
                    services.AddScoped<IDriveControllerService, DriveControllerService>();
                    services.AddScoped<IComponentControllerService, ComponentControllerService>();
                    services.AddScoped<IOrderControllerService, OrderControllerService>();
                    services.AddScoped<IListViewControllerService, ListViewService>();
                    services.AddScoped<IBrakeControllerService, BrakeControllerService>();
                    services.AddScoped<IDriveControllerService, DriveControllerService>();
                    services.AddScoped<IExternalCoolingControllerService, ExternalCoolingControllerService>();
                    services.AddScoped<IInverterControllerService, InverterControllerService>();
                    services.AddScoped<IPumpControllerService, PumpControllerService>();
                    services.AddScoped<ISupplementControllerService, SupplementControllerService>();
                    services.AddScoped<IVariatorControllerService, VariatorControllerService>();

                    services.AddScoped<ICatalogService, CatalogService>();
                    services.AddScoped<ILockControllerService, LockControllerService>();
                    services.AddScoped<ILookupControllerService, LookupControllerService>();
                })
                .Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;




            Console.WriteLine("Import start.");


            var orderService = services.GetRequiredService<IOrderControllerService>();
            await orderService.ImportOrders();

           
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
    }
}
