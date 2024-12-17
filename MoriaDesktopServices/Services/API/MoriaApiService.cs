using MoriaBaseServices;
using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;

namespace MoriaDesktopServices.Services.API;

/// <summary>
/// Middle layer between <see cref="ApiRequestService"/> and MoriaApi controller specific services 
/// </summary>
public class MoriaApiService : IApiService
{
    readonly IApiCredentialsService _credentialsService;
    readonly ApiRequestService _requestService;
    readonly ITokensManager _tokensManager;

    public MoriaApiService(IApiCredentialsService credentialsService, ApiRequestService requestService, ITokensManager tokensManager)
    {
        _credentialsService = credentialsService;
        _requestService = requestService;
        _tokensManager = tokensManager;
    }

    public async Task<T> Get<T>(string username, string endpointPath, Dictionary<string, string> headers, params object[] parameters)
    {
        if(!string.IsNullOrWhiteSpace(username))
            AddAuthorizationHeader(ref headers, username);
        return await _requestService.Get<T>(_credentialsService.GetScheme(), _credentialsService.GetHost(), _credentialsService.GetPortNumber(), endpointPath, headers, parameters);
    }

    public async Task<string> Get(string username, string endpointPath, Dictionary<string, string> headers, params object[] parameters)
    {
        if (!string.IsNullOrWhiteSpace(username))
            AddAuthorizationHeader(ref headers, username);
        return await _requestService.Get(_credentialsService.GetScheme(), _credentialsService.GetHost(), _credentialsService.GetPortNumber(), endpointPath, headers, parameters);
    }

    public async Task<T> Post<T>(string username, string endpointPath, Dictionary<string, string> headers, object data, params object[] parameters)
    {
        if (!string.IsNullOrWhiteSpace(username))
            AddAuthorizationHeader(ref headers, username);
        return await _requestService.Post<T>(_credentialsService.GetScheme(), _credentialsService.GetHost(), _credentialsService.GetPortNumber(), endpointPath, headers, _requestService.GetStringHttpContent(data), parameters);
    }

    public async Task<string> Post(string username, string endpointPath, Dictionary<string, string> headers, object data, params object[] parameters)
    {
        if (!string.IsNullOrWhiteSpace(username))
            AddAuthorizationHeader(ref headers, username);
        return await _requestService.Post(_credentialsService.GetScheme(), _credentialsService.GetHost(), _credentialsService.GetPortNumber(), endpointPath, headers, _requestService.GetStringHttpContent(data), parameters);
    }

    public async Task<T> Put<T>(string username, string endpointPath, Dictionary<string, string> headers, object data, params object[] parameters)
    {
        if (!string.IsNullOrWhiteSpace(username))
            AddAuthorizationHeader(ref headers, username);
        return await _requestService.Put<T>(_credentialsService.GetScheme(), _credentialsService.GetHost(), _credentialsService.GetPortNumber(), endpointPath, headers, _requestService.GetStringHttpContent(data), parameters);
    }

    public async Task<string> Put(string username, string endpointPath, Dictionary<string, string> headers, object data, params object[] parameters)
    {
        if (!string.IsNullOrWhiteSpace(username))
            AddAuthorizationHeader(ref headers, username);
        return await _requestService.Put(_credentialsService.GetScheme(), _credentialsService.GetHost(), _credentialsService.GetPortNumber(), endpointPath, headers, _requestService.GetStringHttpContent(data), parameters);
    }

    public async Task<T> Delete<T>(string username, string endpointPath, Dictionary<string, string> headers, params object[] parameters)
    {
        if (!string.IsNullOrWhiteSpace(username))
            AddAuthorizationHeader(ref headers, username);
        return await _requestService.Delete<T>(_credentialsService.GetScheme(), _credentialsService.GetHost(), _credentialsService.GetPortNumber(), endpointPath, headers, parameters);
    }

    public async Task<string> Delete(string username, string endpointPath, Dictionary<string, string> headers, params object[] parameters)
    {
        if (!string.IsNullOrWhiteSpace(username))
            AddAuthorizationHeader(ref headers, username);
        return await _requestService.Delete(_credentialsService.GetScheme(), _credentialsService.GetHost(), _credentialsService.GetPortNumber(), endpointPath, headers, parameters);
    }

    public void AddAuthorizationHeader(ref Dictionary<string, string> headers, string username)
    {
        if (headers == null)
            headers = new();

        var tokenObject = _tokensManager.GetToken(username);
        if (tokenObject == null || string.IsNullOrEmpty(tokenObject.Token))
        {
            if (tokenObject?.ValidTo.AddMinutes(1) < DateTime.Now)
                _tokensManager.RemoveToken(username);

            throw new MoriaAppException(MoriaAppExceptionReason.AuthorizationTokenNotAvailable, "Authorization token not available or expired");
        }

        var authHeader = _requestService.GetAuthorizationHeader(tokenObject.Token);
        headers.Add(authHeader.Key, authHeader.Value);
    }
}
