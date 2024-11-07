namespace MoriaServices.Interfaces
{
    public interface ICredentialsService
    {
        string GetServerName();
        string GetDatabaseName();
        bool GetIsWindowsAuthenticated();
        string GetDatabaseUsername();
        string GetDatabasePassword();
        string GetMoriaUsername();
        string GetMoriaPassword();
    }
}
