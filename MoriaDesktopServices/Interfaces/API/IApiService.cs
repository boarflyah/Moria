namespace MoriaDesktopServices.Interfaces.API;

/// <summary>
/// Stores methods for CRUD operations for specific webapi
/// </summary>
public interface IApiService
{
    void AddAuthorizationHeader(ref Dictionary<string, string> headers, string username);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="username">when <see cref="string.Empty"/> api endpoint do not need authorization</param>
    /// <param name="endpointPath"></param>
    /// <param name="headers"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task<string> Delete(string username, string endpointPath, Dictionary<string, string> headers, params object[] parameters);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="username">when <see cref="string.Empty"/> api endpoint do not need authorization</param>
    /// <param name="endpointPath"></param>
    /// <param name="headers"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task<T> Delete<T>(string username, string endpointPath, Dictionary<string, string> headers, params object[] parameters);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="username">when <see cref="string.Empty"/> api endpoint do not need authorization</param>
    /// <param name="endpointPath"></param>
    /// <param name="headers"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task<string> Get(string username, string endpointPath, Dictionary<string, string> headers, object data, params object[] parameters);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="username">when <see cref="string.Empty"/> api endpoint do not need authorization</param>
    /// <param name="endpointPath"></param>
    /// <param name="headers"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task<T> Get<T>(string username, string endpointPath, Dictionary<string, string> headers, object data, params object[] parameters);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <param name="endpointPath">when <see cref="string.Empty"/> api endpoint do not need authorization</param>
    /// <param name="headers"></param>
    /// <param name="data"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task<string> Post(string username, string endpointPath, Dictionary<string, string> headers, object data, params object[] parameters);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="username">when <see cref="string.Empty"/> api endpoint do not need authorization</param>
    /// <param name="endpointPath"></param>
    /// <param name="headers"></param>
    /// <param name="data"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task<T> Post<T>(string username, string endpointPath, Dictionary<string, string> headers, object data, params object[] parameters);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="username">when <see cref="string.Empty"/> api endpoint do not need authorization</param>
    /// <param name="endpointPath"></param>
    /// <param name="headers"></param>
    /// <param name="data"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task<string> Put(string username, string endpointPath, Dictionary<string, string> headers, object data, params object[] parameters);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="username">when <see cref="string.Empty"/> api endpoint do not need authorization</param>
    /// <param name="endpointPath"></param>
    /// <param name="headers"></param>
    /// <param name="data"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task<T> Put<T>(string username, string endpointPath, Dictionary<string, string> headers, object data, params object[] parameters);
    Dictionary<string, IEnumerable<string>> GetCurrentResponseHeaders();
}