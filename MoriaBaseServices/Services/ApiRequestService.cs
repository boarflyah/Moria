using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace MoriaBaseServices.Services;

/// <summary>
/// Universal service for communicating with external web apis
/// </summary>
public class ApiRequestService
{
    #region consts

    public const string HttpsApiClientName = "CipheredClient";
    public const string JsonMediaTypeName = "application/json";
    const string AuthorizationHeaderName = "Authorization";
    const string BearerTokenName = "Bearer";

    #endregion

    readonly IHttpClientFactory _clientFactory;
    public ApiRequestService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
    }

    #region send request public methods

    /// <summary>
    /// Use get method to send request to api and deserialize response to <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="scheme">http or https</param>
    /// <param name="host"></param>
    /// <param name="port"></param>
    /// <param name="endpointPath">path only without parameters</param>
    /// <param name="headers">use <see cref="GetAuthorizationHeader(string)"/> for bearer token header object</param>
    /// <param name="parameters">array of single parametrs</param>
    /// <returns>Deserialized api response to <typeparamref name="T"/></returns>
    /// <exception cref="MoriaApiException"/>
    public async Task<T> Get<T>(string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, params object[] parameters)
    {
        return await TrySendAsync<T>(HttpMethod.Get, scheme, host, port, endpointPath, headers, null, parameters);
    }

    /// <summary>
    /// Use get method to send request to api and get response as <see cref="string"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="scheme">http or https</param>
    /// <param name="host"></param>
    /// <param name="port"></param>
    /// <param name="endpointPath">path only without parameters</param>
    /// <param name="headers">use <see cref="GetAuthorizationHeader(string)"/> for bearer token header object</param>
    /// <param name="parameters">array of single parametrs</param>
    /// <returns>response read as <see cref="string"/></returns>
    /// <exception cref="MoriaApiException"/>
    public async Task<string> Get(string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, params object[] parameters)
    {
        return await TrySendAsync(HttpMethod.Get, scheme, host, port, endpointPath, headers, null, parameters);
    }

    /// <summary>
    /// Use post method to send request to api and deserialize response to <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="scheme">http or https</param>
    /// <param name="host"></param>
    /// <param name="port"></param>
    /// <param name="endpointPath">path only without parameters</param>
    /// <param name="headers">use <see cref="GetAuthorizationHeader(string)"/> for bearer token header object</param>
    /// <param name="content">use <see cref="GetStringHttpContent(object)"/> for JSON serialized object request body</param>
    /// <param name="parameters">array of single parametrs</param>
    /// <returns>Deserialized api response to <typeparamref name="T"/></returns>
    /// <exception cref="MoriaApiException"/>
    public async Task<T> Post<T>(string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, HttpContent content, params object[] parameters)
    {
        return await TrySendAsync<T>(HttpMethod.Post, scheme, host, port, endpointPath, headers, content, parameters);
    }

    /// <summary>
    /// Use post method to send request to api and get response as <see cref="string"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="scheme">http or https</param>
    /// <param name="host"></param>
    /// <param name="port"></param>
    /// <param name="endpointPath">path only without parameters</param>
    /// <param name="headers">use <see cref="GetAuthorizationHeader(string)"/> for bearer token header object</param>
    /// <param name="content">use <see cref="GetStringHttpContent(object)"/> for JSON serialized object request body</param>
    /// <param name="parameters">array of single parametrs</param>
    /// <returns>response read as <see cref="string"/></returns>
    /// <exception cref="MoriaApiException"/>
    public async Task<string> Post(string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, HttpContent content, params object[] parameters)
    {
        return await TrySendAsync(HttpMethod.Post, scheme, host, port, endpointPath, headers, content, parameters);
    }

    /// <summary>
    /// Use put method to send request to api and deserialize response to <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="scheme">http or https</param>
    /// <param name="host"></param>
    /// <param name="port"></param>
    /// <param name="endpointPath">path only without parameters</param>
    /// <param name="headers">use <see cref="GetAuthorizationHeader(string)"/> for bearer token header object</param>
    /// <param name="content">use <see cref="GetStringHttpContent(object)"/> for JSON serialized object request body</param>
    /// <param name="parameters">array of single parametrs</param>
    /// <returns>Deserialized api response to <typeparamref name="T"/></returns>
    /// <exception cref="MoriaApiException"/>
    public async Task<T> Put<T>(string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, HttpContent content, params object[] parameters)
    {
        return await TrySendAsync<T>(HttpMethod.Put, scheme, host, port, endpointPath, headers, content, parameters);
    }

    /// <summary>
    /// Use put method to send request to api and get response as <see cref="string"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="scheme">http or https</param>
    /// <param name="host"></param>
    /// <param name="port"></param>
    /// <param name="endpointPath">path only without parameters</param>
    /// <param name="headers">use <see cref="GetAuthorizationHeader(string)"/> for bearer token header object</param>
    /// <param name="content">use <see cref="GetStringHttpContent(object)"/> for JSON serialized object request body</param>
    /// <param name="parameters">array of single parametrs</param>
    /// <returns>response read as <see cref="string"/></returns>
    /// <exception cref="MoriaApiException"/>
    public async Task<string> Put(string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, HttpContent content, params object[] parameters)
    {
        return await TrySendAsync(HttpMethod.Put, scheme, host, port, endpointPath, headers, content, parameters);
    }

    /// <summary>
    /// Use delete method to send request to api and deserialize response to <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="scheme">http or https</param>
    /// <param name="host"></param>
    /// <param name="port"></param>
    /// <param name="endpointPath">path only without parameters</param>
    /// <param name="headers">use <see cref="GetAuthorizationHeader(string)"/> for bearer token header object</param>
    /// <param name="content">use <see cref="GetStringHttpContent(object)"/> for JSON serialized object request body</param>
    /// <param name="parameters">array of single parametrs</param>
    /// <returns>Deserialized api response to <typeparamref name="T"/></returns>
    /// <exception cref="MoriaApiException"/>
    public async Task<T> Delete<T>(string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, params object[] parameters)
    {
        return await TrySendAsync<T>(HttpMethod.Delete, scheme, host, port, endpointPath, headers, null, parameters);
    }

    /// <summary>
    /// Use get method to send request to api and get response as <see cref="string"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="scheme">http or https</param>
    /// <param name="host"></param>
    /// <param name="port"></param>
    /// <param name="endpointPath">path only without parameters</param>
    /// <param name="headers">use <see cref="GetAuthorizationHeader(string)"/> for bearer token header object</param>
    /// <param name="parameters">array of single parametrs</param>
    /// <returns>response read as <see cref="string"/></returns>
    /// <exception cref="MoriaApiException"/>
    public async Task<string> Delete(string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, params object[] parameters)
    {
        return await TrySendAsync(HttpMethod.Delete, scheme, host, port, endpointPath, headers, null, parameters);
    }

    #endregion

    #region other public methods

    /// <summary>
    /// Used for preparing request body
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public HttpContent GetStringHttpContent(object data)
    {
        return new StringContent(JsonSerializer.Serialize(data), new System.Net.Http.Headers.MediaTypeHeaderValue(JsonMediaTypeName));
    }

    /// <summary>
    /// Used for preparing authorization header for api request
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public KeyValuePair<string, string> GetAuthorizationHeader(string token)
    {
        return new(AuthorizationHeaderName, $"{BearerTokenName} {token}");
    }

    #endregion

    #region helper methods

    async Task<T> TrySendAsync<T>(HttpMethod method, string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, HttpContent content, params object[] parameters)
    {
        using var response = await SendAsync(method, scheme, host, port, endpointPath, headers, content, parameters);
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

    async Task<string> TrySendAsync(HttpMethod method, string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, HttpContent content, params object[] parameters)
    {
        using var response = await SendAsync(method, scheme, host, port, endpointPath, headers, content, parameters);
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

    async Task<HttpResponseMessage> SendAsync(HttpMethod method, string scheme, string host, int port, string endpointPath, Dictionary<string, string> headers, HttpContent content, params object[] parameters)
    {
        var uri = GetUri(scheme, host, port, endpointPath, parameters);
        using HttpRequestMessage request = new(method, uri);
        if (content != null)
            request.Content = content;

        using var apiClient = GetHttpClient(_clientFactory, scheme);

        apiClient.DefaultRequestHeaders.Clear();
        if (headers != null)
            foreach (var header in headers)
                apiClient.DefaultRequestHeaders.Add(header.Key, header.Value);

        return await apiClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
    }

    HttpClient GetHttpClient(IHttpClientFactory factory, string scheme)
    {
        if (scheme.Equals("https"))
            return factory.CreateClient(HttpsApiClientName);
        else
            return factory.CreateClient();
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

    MoriaApiException GetApiException(string message)
    {
        var exception = JsonSerializer.Deserialize<MoriaApiException>(message);
        if (exception == null)
            throw new MoriaApiException(MoriaApiExceptionReason.Unknown, MoriaApiException.ApiExceptionThrownStatusCode);
        return exception;
    }

    #endregion
}
