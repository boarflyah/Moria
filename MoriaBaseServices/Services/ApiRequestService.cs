using System.Net.Http;

namespace MoriaBaseServices.Services;
public class ApiRequestService
{
    readonly IHttpClientFactory _clientFactory;
    public ApiRequestService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }
}
