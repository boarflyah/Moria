namespace MoriaDesktopServices.Models;
public class TokenObject
{
    public string Username
    {
        get; set;
    }

    public DateTime ValidTo
    {
        get; set;
    }

    public string Token
    {
        get; set;
    }
}
