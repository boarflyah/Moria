using MoriaModelsDo.Base;

namespace MoriaWebAPIServices.Services.Interfaces;
public interface ILockControllerService
{
    Task<bool> Lock(LockHelper lockHelper);
    Task<bool> Unlock(LockHelper lockHelper);
}