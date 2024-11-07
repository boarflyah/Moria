using Microsoft.Extensions.Logging;
using MoriaServices.Interfaces;

namespace MoriaConsoleApp.Services
{
    public class TestCredentialsService : ICredentialsService
    {
        readonly ILogger<TestCredentialsService> _logger;

        public TestCredentialsService(ILogger<TestCredentialsService> logger)
        {
            _logger = logger;
        }

        public string GetDatabaseName() => "Nexo_Test";
        public string GetServerName() => "LAPTOP-AI0I106Q\\INSERTNEXO";
        public bool GetIsWindowsAuthenticated() => true;
        public string GetDatabaseUsername() => string.Empty;
        public string GetDatabasePassword() => string.Empty;
        public string GetMoriaUsername() => "Szef";
        public string GetMoriaPassword() => "123";
    }
}
