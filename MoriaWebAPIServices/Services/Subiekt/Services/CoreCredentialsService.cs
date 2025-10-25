using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MoriaWebAPIServices.Services.Subiekt.Core;
using MoriaWebAPIServices.Services.Subiekt.Interfaces;

namespace MoriaWebAPIServices.Services.Subiekt.Services;

public class CoreCredentialsService : ICredentialsService
{
    private readonly CredentialsConfig _config;

    public CoreCredentialsService(IOptions<CredentialsConfig> options)
    {
        _config = options.Value;
    }

    public string GetDatabaseName() => _config.DatabaseName;
    public string GetDatabasePassword() => _config.DatabasePassword;
    public string GetDatabaseUsername() => _config.DatabaseUsername;
    public bool GetIsWindowsAuthenticated() => _config.IsWindowsAuthenticated;
    public string GetMoriaPassword() => _config.SubiektPassword;
    public string GetMoriaUsername() => _config.SubiektUsername;
    public string GetServerName() => _config.ServerName;

    //public string GetDatabaseName() => ConfigurationManager.AppSettings["DatabaseName"] ?? string.Empty;

    //public string GetDatabasePassword() => ConfigurationManager.AppSettings["DatabasePassword"] ?? string.Empty;

    //public string GetDatabaseUsername() => ConfigurationManager.AppSettings["DatabaseUsername"] ?? string.Empty;

    //public bool GetIsWindowsAuthenticated()
    //{
    //    var authString = ConfigurationManager.AppSettings["IsWindowsAuthenticated"] ?? string.Empty;
    //    bool isWindowsAuthenticated = false;
    //    if (bool.TryParse(authString, out isWindowsAuthenticated))
    //        return isWindowsAuthenticated;
    //    return false;
    //}

    //public string GetMoriaPassword() => ConfigurationManager.AppSettings["SubiektPassword"] ?? string.Empty;

    //public string GetMoriaUsername() => ConfigurationManager.AppSettings["SubiektUsername"] ?? string.Empty;

    //public string GetServerName() => ConfigurationManager.AppSettings["ServerName"] ?? string.Empty;
}
