using MoriaDesktopServices.Models;

namespace MoriaDesktopServices.Interfaces;

/// <summary>
/// Manages api tokens in context of user on local machine
/// </summary>
public interface ITokensManager
{
    void StoreToken(string username, string token, DateTime validTo);
    TokenObject GetToken(string username);
    void RemoveToken(string username);
}
