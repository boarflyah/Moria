using MoriaModelsDo.Models.Contacts;

namespace MoriaWebAPIServices.Services.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeDo> LogIn(string username, string password);

    Task<IEnumerable<EmployeeDo>> GetEmployees();
}
