using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MoriaConsoleApp.Services;
using MoriaServices.Interfaces;
using MoriaServices.Services;
using Serilog;

namespace MoriaConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File("Logs\\log-.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();
                var container = ConfigureContainer();

                var serviceProvider = new AutofacServiceProvider(container);
                var application = serviceProvider.GetService<AppHost>();

                application.Run(args);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IContainer ConfigureContainer()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging(configure =>
            {
                configure.AddSerilog(dispose: true);
                configure.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
            });

            var builder = new ContainerBuilder();
            builder.Populate(serviceCollection);
            builder.RegisterType<AppHost>().AsSelf();
            builder.RegisterType<ConsoleService>().AsSelf();
            builder.RegisterType<TestSalesOrderQueriesService>().As<ISalesOrderQueriesService>();
#if DEBUG
            //builder.RegisterType<TestCredentialsService>().As<ICredentialsService>();
            builder.RegisterType<CoreCredentialsService>().As<ICredentialsService>();
#else
            builder.RegisterType<CoreCredentialsService>().As<ICredentialsService>();
#endif
            builder.RegisterType<MoriaHandlerService>().As<IMoriaHandlerService>();
            builder.RegisterType<DictionariesService>().As<IDictionariesService>();
            builder.RegisterType<SalesOrderService>().As<ISalesOrderService>();

            return builder.Build();
        }
    }
}
