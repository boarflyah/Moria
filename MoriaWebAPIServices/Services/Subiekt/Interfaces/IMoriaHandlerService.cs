
using InsERT.Moria.Sfera;

namespace MoriaWebAPIServices.Services.Subiekt.Interfaces;

public interface IMoriaHandlerService
{
    Uchwyt GetHandler();
    bool Login(Uchwyt handler);
}
