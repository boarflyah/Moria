using MoriaModelsDo.Models.Contacts;

namespace MoriaWebAPIServices.Services.Interfaces.Dictionaries;

public interface IEmployeeControllerService
{
    Task<EmployeeDo> LogIn(string username, string password);

    Task<IEnumerable<EmployeeDo>> GetEmployees();

    Task<EmployeeDo> GetEmployee(int id);

    Task<EmployeeDo> CreateEmployee(EmployeeDo employee);

    Task<EmployeeDo> UpdateEmployee(EmployeeDo employee);

    Task<bool> DeleteEmployee(int id);
}