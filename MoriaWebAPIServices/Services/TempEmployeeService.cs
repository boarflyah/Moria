using MoriaModelsDo.Models.Contacts;
using MoriaWebAPIServices.Services.Interfaces;

namespace MoriaWebAPIServices.Services;
public class TempEmployeeService : IEmployeeService
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

    public async Task<IEnumerable<EmployeeDo>> GetEmployees()
    {
        var result = new List<EmployeeDo>();
        result.Add(new()
        {
            Id = 1,
            FirstName = "Arek",
            LastName = "Nowy",
            PhoneNumber = "346125654",
            Username = "ANO",
        });
        result.Add(new()
        {
            Id = 1,
            FirstName = "Krzysiek",
            LastName = "Kowalski",
            PhoneNumber = "32643253",
            Username = "KKO",
            IsLocked = true
        });
        ;
        result.Add(new()
        {
            Id = 1,
            FirstName = "Jan",
            LastName = "Nowak",
            PhoneNumber = "89754573",
            Username = "JNO",
        });

        return result;
    }

}
