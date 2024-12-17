using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktopServices.Interfaces.API;

/// <summary>
/// Used for reading enpoints in TokenController in MoriaApi
/// </summary>
public interface IApiTokenService
{
    Task<EmployeeDo> GetUserWithToken(string username, string password);
}
