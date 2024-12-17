using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Models;

namespace MoriaDesktopServices.Services;

/// <summary>
/// Manages api tokens in context of user on local machine
/// </summary>
public class TokensManager : ITokensManager
{
    const string fileName = "tokens.dat";

    /// <summary>
    /// Gets token for specified user if exists
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public TokenObject GetToken(string username)
    {
        if (!File.Exists(fileName))
            return null;

        var tokens = GetTokens();

        return tokens?.FirstOrDefault(x => x.Username == username);
    }
    public void RemoveToken(string username)
    {
        if (!File.Exists(fileName))
            return;

        var tokens = GetTokens();
        var tokenObject = tokens.FirstOrDefault(x => x.Username == username);
        if (tokenObject != null)
            tokens.Remove(tokenObject);

        byte[] data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(tokens));
        byte[] encryptedData = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);

        File.WriteAllBytes(fileName, encryptedData);
    }

    /// <summary>
    /// Adds or updates token into tokens file for specified user
    /// </summary>
    /// <param name="username"></param>
    /// <param name="token"></param>
    /// <param name="validTo"></param>
    public void StoreToken(string username, string token, DateTime validTo)
    {
        IList<TokenObject> tokens = null;
        if (File.Exists(fileName))
            tokens = GetTokens();

        if (tokens == null)
            tokens = new List<TokenObject>();

        var tokenObject = tokens.FirstOrDefault(x => x.Username == username);
        if (tokenObject != null)
        {
            tokenObject.Token = token;
            tokenObject.ValidTo = validTo;
        }
        else
            tokens.Add(new()
            {
                Username = username,
                Token = token,
                ValidTo = validTo,
            });

        byte[] data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(tokens));
        byte[] encryptedData = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);

        File.WriteAllBytes(fileName, encryptedData);
    }

    /// <summary>
    /// Retrieving all tokens from file
    /// </summary>
    /// <returns></returns>
    IList<TokenObject> GetTokens()
    {
        byte[] encryptedData = File.ReadAllBytes(fileName);
        byte[] decryptedData = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);

        var tokensString = Encoding.UTF8.GetString(decryptedData);
        if (string.IsNullOrWhiteSpace(tokensString))
            return null;

        return JsonSerializer.Deserialize<IList<TokenObject>>(tokensString);
    }
}
