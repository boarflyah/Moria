using MoriaModelsDo.Models.Contacts;

namespace MoriaWebAPIServices.Services.Interfaces;

public interface IUserService
{
    Task<EmployeeDo> LogIn(string username, string password);
}
