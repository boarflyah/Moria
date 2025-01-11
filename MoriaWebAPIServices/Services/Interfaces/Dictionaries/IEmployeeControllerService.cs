using MoriaModelsDo.Models.Contacts;

namespace MoriaWebAPIServices.Services.Interfaces.Dictionaries;

public interface IEmployeeControllerService
{
    Task<EmployeeDo> LogIn(string username, string password);

    Task<IEnumerable<EmployeeDo>> GetEmployees();

    Task<EmployeeDo> GetEmployee(int id);

    Task<bool> CreateEmployee(EmployeeDo employee);
}