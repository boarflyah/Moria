using MoriaWebAPIServices.Services.Interfaces;

namespace MoriaWebAPIServices.Services;
public class TempUserService : IUserService
{
    public string LogIn(string username, string password)
    {
        if (username.Equals("123") && password.Equals("abc"))
            return "70054d2f-0c8e-4d82-bb0d-6ad3fe7b8f66";
        else
            return string.Empty;
    }
}
