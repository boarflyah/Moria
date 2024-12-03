using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktopServices.Interfaces.API;

/// <summary>
/// Used for reading enpoints in EmployeeController in MoriaApi
/// </summary>
public interface IApiEmployeeService
{
    Task<EmployeeDo> Login(string username, string password);
}
