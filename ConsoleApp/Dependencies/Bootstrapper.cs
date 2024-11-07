using Autofac;

namespace ConsoleApp.Dependencies
{
    public class Bootstrapper
    {
        public static ContainerBuilder RegisterContainerBuilder()
        {
            ContainerBuilder builder = new ContainerBuilder();
            //builder.Register(c => new MoriaHandlerService()).As<IMoriaHandlerService>();
            //builder.Register(c => new ContractsService
            //                (c.Resolve<IMoriaHandlerService>())).As<ISalesOrder>();
            return builder;
        }
    }
}
