namespace MoriaDesktopServices.Interfaces.API;
public interface IApiLockService
{
    Task<bool> Lock(string username, Type modelType, int id);

    Task<bool> Unlock(string username, Type modelType, int id);
}
