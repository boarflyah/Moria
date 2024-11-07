using MoriaServices.Interfaces;

namespace MoriaWCFApp.Services
{
    public class TestService : ITestService
    {
        public TestService(ICredentialsService credentialsService)
        {
            ;
        }

        public string Get(string data) => $"Podany tekst: {data}";
    }
}
