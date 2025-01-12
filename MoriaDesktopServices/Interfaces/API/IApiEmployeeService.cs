using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktopServices.Interfaces.API;

/// <summary>
/// Used for reading enpoints in EmployeeController in MoriaApi
/// </summary>
public interface IApiEmployeeService
{
    Task<EmployeeDo> Login(string username, string password);
    Task<IEnumerable<EmployeeDo>> GetEmployees(string username);
    Task<EmployeeDo> GetEmployee(string username, int id);
    Task<EmployeeDo> CreateEmployee(string username, EmployeeDo employee);
    Task<EmployeeDo> UpdateEmployee(string username, EmployeeDo employee);
    Task<bool> DeleteEmployee(string username, int id);
}
