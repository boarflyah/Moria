using System;
using System.ServiceModel;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Integration.Wcf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MoriaServices.Interfaces;
using MoriaServices.Services;
using MoriaWcfContracts.Services;
using MoriaWCFContracts.Interfaces;
using MoriaWCFContracts.Services;
using Serilog;

namespace MoriaWCFApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();


                var container = ConfigureContainer();

                // Tworzenie hosta WCF
                using (ServiceHost host = new ServiceHost(typeof(ContractsService)))
                {
                    // Konfiguracja hosta WCF z Autofac do Dependency Injection
                    host.AddDependencyInjectionBehavior<ISalesOrderContract>(container);

                    // Uruchamianie hosta
                    host.Open();
                    Console.WriteLine("WCF Service is running...");

                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();

                    host.Close();
                }
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IContainer ConfigureContainer()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging(configure =>
            {
                configure.AddSerilog(dispose: true);
                configure.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
            });

            var builder = new ContainerBuilder();
            builder.Populate(serviceCollection);
#if DEBUG
            //builder.RegisterType<TestCredentialsService>().As<ICredentialsService>();
            builder.RegisterType<CoreCredentialsService>().As<ICredentialsService>();
#else
            builder.RegisterType<CoreCredentialsService>().As<ICredentialsService>();
#endif
            builder.RegisterType<MoriaHandlerService>().As<IMoriaHandlerService>();
            builder.RegisterType<TestSalesOrderQueriesService>().As<ISalesOrderQueriesService>();
            builder.RegisterType<DictionariesService>().As<IDictionariesService>();
            builder.RegisterType<SalesOrderService>().As<ISalesOrderService>();

            //Rejestracja klasy WCF
            builder.RegisterType<ContractsService>().As<ISalesOrderContract>();

            return builder.Build();
        }
    }
}
