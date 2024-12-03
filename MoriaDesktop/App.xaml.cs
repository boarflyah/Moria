using System.IO;
using System.Net.Http;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoriaBaseServices.Services;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.Views.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaDesktopServices.Services;
using MoriaDesktopServices.Services.API;

namespace MoriaDesktop;

public partial class App : Application
{
    public readonly IHost? AppHost;

    public App()
    {

       AppHost = Host.CreateDefaultBuilder()
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddSingleton<MainWindow>();
                        services.AddSingleton<MainWindowViewModel>();
                        services.AddSingleton<AppStateService>();
                        services.AddScoped<LoginView>();
                        services.AddScoped<LoginViewModel>();
                        services.AddScoped<SecondView>();
                        services.AddScoped<SecondViewModel>();
                        services.AddScoped<IPageService, DesktopPageService>();
                        services.AddSingleton<INavigationService, NavigationService>();
                        services.AddScoped<ApiRequestService>();
                        services.AddScoped<IApiCredentialsService, MoriaApiCredentialsService>();
                        services.AddScoped<IApiService, MoriaApiService>();
                        services.AddScoped<IApiTokenService, ApiTokenService>();
                        services.AddScoped<ApiTestService>();
                        services.AddScoped<ITokensManager, TokensManager>();
                        services.AddScoped<IApiEmployeeService, ApiEmployeeService>();
                        services.AddHttpClient();
                        services.AddHttpClient(ApiRequestService.HttpsApiClientName)
                        .ConfigurePrimaryHttpMessageHandler(c =>
                        {
                            var credentialsService = c.GetService<IApiCredentialsService>();
                            return new HttpClientHandler
                            {
                                ServerCertificateCustomValidationCallback = (m, c, ch, e) =>
                                {
                                    if (c.Thumbprint.ToLower() == credentialsService?.GetCertificateThumbprint())
                                        return true;
                                    else
                                        return false;
                                }
                            };
                        });
                    })
                    .ConfigureAppConfiguration((context, config) =>
                    {
                        config.SetBasePath(Directory.GetCurrentDirectory());
                        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    })
                    .Build();
    }

    protected async override void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var wnd = AppHost.Services.GetRequiredService<MainWindow>();
        var navigationService = AppHost.Services.GetRequiredService<INavigationService>();
        navigationService.SetFrame(wnd.NavigationFrame);

        wnd.Title = "MoriaDesktop";
        wnd.Resources.MergedDictionaries.Add(Application.Current.Resources);
        wnd.Show();
        base.OnStartup(e);
    }

}