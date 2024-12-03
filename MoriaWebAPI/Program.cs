using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MoriaWebAPI.Services;
using MoriaWebAPI.Services.Interfaces;
using MoriaWebAPIServices.Services;
using MoriaWebAPIServices.Services.Interfaces;
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
                //.MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(".\\Logs\\log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(Log.Logger);

            // Add services to the container.
            builder.Services.AddScoped<IUserService, TempUserService>();
            builder.Services.AddScoped<ITokenGeneratorService, TempTokenGeneratorService>(serviceProvider =>
            {
                TempTokenGeneratorService service = new(ip);
                return service;
            });

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
