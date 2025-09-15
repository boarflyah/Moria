using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoriaWebAPI.Controllers;
using MoriaWebAPI.Services;
using MoriaWebAPI.Services.Interfaces;
using MoriaWebAPIServices.Contexts;
using MoriaWebAPIServices.Services;
using MoriaWebAPIServices.Services.Dictionaries;
using MoriaWebAPIServices.Services.DriveComponents;
using MoriaWebAPIServices.Services.HostedService;
using MoriaWebAPIServices.Services.Interfaces;
using MoriaWebAPIServices.Services.Interfaces.Dictionaries;
using MoriaWebAPIServices.Services.Interfaces.DriveComponents;
using MoriaWebAPIServices.Services.Interfaces.Orders;
using MoriaWebAPIServices.Services.Interfaces.Products;
using MoriaWebAPIServices.Services.Orders;
using MoriaWebAPIServices.Services.Products;
using Serilog;

namespace MoriaWebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);
            var ip = GetLocalIPAddress();
            var portNumber = builder.Configuration.GetValue(typeof(int), "PortNumber") as int?;

            builder.Host.UseWindowsService();

            //logger configuration
            Log.Logger = new LoggerConfiguration()
#if !DEBUG
                .MinimumLevel.Warning()
#endif
                //.MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("C:\\Apps\\Logs\\WebApi\\log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(Log.Logger);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("MoriaDatabase"));
            });
            builder.Services.AddSingleton<ModelTypeConverter>();
            builder.Services.AddScoped<ModelsCreator>();
            builder.Services.AddSingleton<LockService>();
            builder.Services.AddScoped<IEmployeeControllerService, EmployeeControllerService>();
            builder.Services.AddScoped<IWarehouseControllerService, WarehouseControllerService>();
            builder.Services.AddScoped<IColorControllerService, ColorControllerService>();
            builder.Services.AddScoped<IContactControllerService, ContactControllerService>();
            builder.Services.AddScoped<IMotorControllerService, MotorControllerService>();
            builder.Services.AddScoped<IMotorGearControllerService, MotorGearControllerService>();
            builder.Services.AddScoped<IPositionControllerService, PositionControllerService>();
            builder.Services.AddScoped<ISteelKindControllerService, SteelKindControllerService>();
            builder.Services.AddScoped<IProductControllerService, ProductControllerService>();
            builder.Services.AddScoped<IElectricalCabinetControllerService, ElectricalCabinetControllerService>();
            builder.Services.AddScoped<ICategoryControllerService, CategoryControllerService>();
            builder.Services.AddScoped<IDriveControllerService, DriveControllerService>();
            builder.Services.AddScoped<IComponentControllerService, ComponentControllerService>();
            builder.Services.AddScoped<IOrderControllerService, OrderControllerService>();
            builder.Services.AddScoped<IListViewControllerService, ListViewService>();
            builder.Services.AddScoped<IBrakeControllerService, BrakeControllerService>();
            builder.Services.AddScoped<IDriveControllerService, DriveControllerService>();
            builder.Services.AddScoped<IExternalCoolingControllerService, ExternalCoolingControllerService>();
            builder.Services.AddScoped<IInverterControllerService, InverterControllerService>();
            builder.Services.AddScoped<IPumpControllerService, PumpControllerService>();
            builder.Services.AddScoped<ISupplementControllerService, SupplementControllerService>();
            builder.Services.AddScoped<IVariatorControllerService, VariatorControllerService>();

            builder.Services.AddHostedService<BackupDatabaseService>();

            builder.Services.AddScoped<ICatalogService, CatalogService>();
            builder.Services.AddScoped<ILockControllerService, LockControllerService>();
            builder.Services.AddScoped<ILookupControllerService, LookupControllerService>();
            builder.Services.AddScoped<ITokenGeneratorService, TempTokenGeneratorService>(serviceProvider =>
            {
                TempTokenGeneratorService service = new(ip);
                return service;
            });

#if RELEASE
            builder.Services.AddHostedService<SubiektOrderUpdater>();
#endif
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //authentication configuration
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new()
                    {
                        ValidIssuer = ip,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey123456789101112131415")),
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        //by default ClockSkew is set for 5 minutes, it means that token is valid even 5 minutes later, than token expiration date
                        ClockSkew = TimeSpan.FromSeconds(60),
                    };
                });
            builder.Services.AddAuthorization();

            //kestrel config, port/ip/certificates
            builder.WebHost.ConfigureKestrel((context, options) =>
            {
                options.Listen(IPAddress.Parse(ip), portNumber.GetValueOrDefault(5000), configure =>
                {
                    //configure.UseHttps();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var lockService = app.Services.GetRequiredService<LockService>();
            lockService.Start();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                return ip.ToString();
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }

}
