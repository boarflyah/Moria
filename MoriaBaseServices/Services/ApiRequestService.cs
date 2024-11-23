using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace MoriaBaseServices.Services;
public class ApiRequestService
{
    readonly IHttpClientFactory _clientFactory;
    public ApiRequestService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
    }

    public async Task<T> Get<T>(string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, params object[] parameters)
    {
        return await TrySendAsync<T>(HttpMethod.Get, scheme, host, port, endpointPath, headers, string.Empty, null, parameters);
    }

    public async Task<string> Get(string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, params object[] parameters)
    {
        return await TrySendAsync(HttpMethod.Get, scheme, host, port, endpointPath, headers, string.Empty, null, parameters);
    }

    public async Task<T> Post<T>(string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, string mediaType, object data, params object[] parameters)
    {
        return await TrySendAsync<T>(HttpMethod.Post, scheme, host, port, endpointPath, headers, mediaType, data, parameters);
    }

    public async Task<string> Post(string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, string mediaType, object data, params object[] parameters)
    {
        return await TrySendAsync(HttpMethod.Post, scheme, host, port, endpointPath, headers, mediaType, data, parameters);
    }

    public async Task<T> Put<T>(string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, string mediaType, object data, params object[] parameters)
    {
        return await TrySendAsync<T>(HttpMethod.Put, scheme, host, port, endpointPath, headers, mediaType, data, parameters);
    }

    public async Task<string> Put(string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, string mediaType, object data, params object[] parameters)
    {
        return await TrySendAsync(HttpMethod.Put, scheme, host, port, endpointPath, headers, mediaType, data, parameters);
    }

    public async Task<T> Delete<T>(string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, params object[] parameters)
    {
        return await TrySendAsync<T>(HttpMethod.Delete, scheme, host, port, endpointPath, headers, string.Empty, null, parameters);
    }

    public async Task<string> Delete(string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, params object[] parameters)
    {
        return await TrySendAsync(HttpMethod.Delete, scheme, host, port, endpointPath, headers, string.Empty, null, parameters);
    }

    async Task<T> TrySendAsync<T>(HttpMethod method, string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, string mediaType, object data, params object[] parameters)
    {
        using var response = await SendAsync(method, scheme, host, port, endpointPath, headers, mediaType, data, parameters);
        try
        {
            response.EnsureSuccessStatusCode();

            var responseDeserialized = await response.Content.ReadFromJsonAsync<T>();
            if (responseDeserialized == null)
                return default;
            return responseDeserialized;
        }
        catch (HttpRequestException ex) when ((int)ex.StatusCode == 700)
        {
            var message = await response.Content.ReadAsStringAsync();
            throw GetApiException(message);
        }
    }

    async Task<string> TrySendAsync(HttpMethod method, string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, string mediaType, object data, params object[] parameters)
    {
        using var response = await SendAsync(method, scheme, host, port, endpointPath, headers, mediaType, data, parameters);
        try
        {
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) when ((int)ex.StatusCode == 700)
        {
            var message = await response.Content.ReadAsStringAsync();
            throw GetApiException(message);
        }
    }

    async Task<HttpResponseMessage> SendAsync(HttpMethod method, string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, string mediaType, object data, params object[] parameters)
    {
        var uri = GetUri(scheme, host, port, endpointPath, parameters);
        using HttpRequestMessage request = new(method, uri);
        if (data != null)
            request.Content = new StringContent(JsonSerializer.Serialize(data), new System.Net.Http.Headers.MediaTypeHeaderValue(mediaType));

        using var apiClient = _clientFactory.CreateClient();

        apiClient.DefaultRequestHeaders.Clear();
        if (headers != null)
            foreach (var header in headers)
                apiClient.DefaultRequestHeaders.Add(header.Key, header.Value);

        return await apiClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
    }

    Uri GetUri(string scheme, string host, int port, string endpointPath, params object[] parameters)
    {
        UriBuilder builder = new()
        {
            Scheme = scheme,
            Host = host,
            Port = port,
            Path = endpointPath
        };
        if (parameters.Any())
            builder.Path += $"{string.Join("/", parameters)}";

        return builder.Uri;
    }

    ApiException GetApiException(string message)
    {
        var exception = JsonSerializer.Deserialize<ApiException>(message);
        if (exception == null)
            throw new ApiException(ApiExceptionReason.Unknown, ApiException.ApiExceptionThrownStatusCode);
        return exception;
    }
}
