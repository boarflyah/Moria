using Moria.MiddlewareService.Interfaces.Services;
using Unity;
using Unity.Wcf;

namespace Moria.MiddlewareService.Services
{
    public class WcfServiceFactory : UnityServiceHostFactory
    {
        protected override void ConfigureContainer(IUnityContainer container)
        {
            container
            .RegisterType<IMoriaHandlerService, MoriaHandlerService>();
        }
    }
}
