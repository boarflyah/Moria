using InsERT.Moria.Sfera;

namespace MoriaServices.Interfaces
{
    public interface IMoriaHandlerService
    {
        Uchwyt GetHandler();
        bool Login(Uchwyt handler);
    }
}
