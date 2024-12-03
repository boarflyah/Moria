using Microsoft.Extensions.Configuration;
using MoriaDesktopServices.Interfaces.API;

namespace MoriaDesktopServices.Services.API;

/// <summary>
/// Stores methods for getting moria api host parameters
/// </summary>
public class MoriaApiCredentialsService : IApiCredentialsService
{
    readonly IConfiguration _configuration;

    public MoriaApiCredentialsService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetHost() => _configuration.GetValue<string>("MoriaApiHost") ?? "";
    public int GetPortNumber() => _configuration.GetValue<int>("MoriaPortNumber");
    public string GetScheme() => _configuration.GetValue<string>("MoriaApiScheme") ?? "";
    public string GetCertificateThumbprint() => _configuration.GetValue<string>("MoriaApiCertificate") ?? "";
}
