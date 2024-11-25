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
    /// <summary>
    /// just for now certificate thumbprint will be stored in configuration file
    /// </summary>
    /// <returns></returns>
    public string GetCertificateThumbprint() => _configuration.GetValue<string>("MoriaApiCertificate") ?? "";
    /// <summary>
    /// just for now token info will be stored in configuration file
    /// </summary>
    /// <returns></returns>
    public string GetToken(string username) => _configuration.GetValue<string>("MoriaApiToken") ?? "";
    /// <summary>
    /// just for now token info will be stored in configuration file
    /// </summary>
    /// <returns></returns>
    public DateTime GetTokenValidTo(string username) => _configuration.GetValue<DateTime>("MoriaTokenValidTo");
}
