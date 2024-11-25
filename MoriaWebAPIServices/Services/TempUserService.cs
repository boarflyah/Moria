using MoriaModelsDo.Models.Contacts;
using MoriaWebAPIServices.Services.Interfaces;

namespace MoriaWebAPIServices.Services;
public class TempUserService : IUserService
{
    public async Task<EmployeeDo> LogIn(string username, string password)
    {
        if (username.Equals("123") && password.Equals("abc"))
            return new()
            {
                Id = 50,
                FirstName = "Jan",
                LastName = "Nowak",
                Username = "123",
            };
        else
            return null;
    }
}
