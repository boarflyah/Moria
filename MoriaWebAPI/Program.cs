using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;

namespace MoriaWebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseWindowsService();

            //logger configuration
            Log.Logger = new LoggerConfiguration()
                //.MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(".\\Logs\\log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(Log.Logger);

            // Add services to the container.


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

            builder.WebHost.ConfigureKestrel((context, options) =>
            {
                var ip = GetLocalIPAddress();
                var portNumber = builder.Configuration.GetValue(typeof(int), "PortNumber") as int?;
                options.Listen(IPAddress.Parse(ip), portNumber.GetValueOrDefault(5000), configure =>
                {
                    configure.UseHttps();
                });
            });

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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
