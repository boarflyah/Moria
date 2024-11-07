using System.Configuration;
using MoriaServices.Interfaces;

namespace MoriaServices.Services
{
    public class CoreCredentialsService : ICredentialsService
    {
        public string GetDatabaseName() => ConfigurationManager.AppSettings["DatabaseName"] ?? string.Empty;

        public string GetDatabasePassword() => ConfigurationManager.AppSettings["DatabasePassword"] ?? string.Empty;

        public string GetDatabaseUsername() => ConfigurationManager.AppSettings["DatabaseUsername"] ?? string.Empty;

        public bool GetIsWindowsAuthenticated()
        {
            var authString = ConfigurationManager.AppSettings["IsWindowsAuthenticated"] ?? string.Empty;
            bool isWindowsAuthenticated = false;
            if (bool.TryParse(authString, out isWindowsAuthenticated))
                return isWindowsAuthenticated;
            return false;
        }

        public string GetMoriaPassword() => ConfigurationManager.AppSettings["SubiektPassword"] ?? string.Empty;

        public string GetMoriaUsername() => ConfigurationManager.AppSettings["SubiektUsername"] ?? string.Empty;

        public string GetServerName() => ConfigurationManager.AppSettings["ServerName"] ?? string.Empty;
    }
}
