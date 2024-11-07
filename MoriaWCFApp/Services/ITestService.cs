using System.ServiceModel;

namespace MoriaWCFApp.Services
{
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        string Get(string data);
    }
}
