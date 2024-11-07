using System;
using System.ServiceModel;
using System.ServiceProcess;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Integration.Wcf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MoriaServices.Interfaces;
using MoriaServices.Services;
using Serilog;
using MoriaWCFContracts.Interfaces;
using MoriaWCFContracts.Services;
using MoriaWcfContracts.Services;

namespace MoriaWCFService
{
    public partial class Service1 : ServiceBase
    {
        ServiceHost host;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.EventLog("MoriaWCFService", manageEventSource: true)
                    .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();
                Log.Information("Starting");
                var container = ConfigureContainer();

                // Tworzenie hosta WCF
                host = new ServiceHost(typeof(ContractsService));

                // Konfiguracja hosta WCF z Autofac do Dependency Injection
                host.AddDependencyInjectionBehavior<ISalesOrderContract>(container);
                Log.Information("Przed otworzeniem hosta");

                // Uruchamianie hosta
                host.Open();
                Log.Information("Po otworzeniu hosta");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Błąd podczas uruchamiania");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        protected override void OnStop()
        {
            host?.Close();
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
            builder.RegisterType<TestSalesOrderQueriesService>().As<ISalesOrderQueriesService>();
            builder.RegisterType<MoriaHandlerService>().As<IMoriaHandlerService>();
            builder.RegisterType<DictionariesService>().As<IDictionariesService>();
            builder.RegisterType<SalesOrderService>().As<ISalesOrderService>();

            //Rejestracja klasy WCF
            builder.RegisterType<ContractsService>().As<ISalesOrderContract>();

            return builder.Build();
        }

    }
}
