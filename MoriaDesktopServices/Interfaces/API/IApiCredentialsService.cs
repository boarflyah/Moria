namespace MoriaDesktopServices.Interfaces.API;

/// <summary>
/// Stores methods for getting api host parameters
/// </summary>
public interface IApiCredentialsService
{
    string GetScheme();
    string GetHost();
    int GetPortNumber();
    string GetCertificateThumbprint();
}
