namespace ConsoleApp
{
    internal class Program
    {

        static void Main(string[] args)
        {

            //ContainerBuilder builder = new ContainerBuilder();
            //builder.RegisterType<Logger<Program>>().As<ILogger>();
            //builder.RegisterType<ContractsService>();

            //using (var container = builder.Build())
            //{
            //    Uri address = new Uri("http://localhost:8080/Service1");
            //    ServiceHost host = new ServiceHost(typeof(ContractsService), address);
            //    //host.AddServiceEndpoint(typeof(ISalesOrder), new BasicHttpBinding(), string.Empty);

            //    // Here's the important part - attaching the DI behavior to the service host
            //    // and passing in the container.
            //    host.AddDependencyInjectionBehavior<ISalesOrder>(container);

            //    host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true, HttpGetUrl = address });
            //    host.Open();

            //    Console.WriteLine("The host has been opened.");
            //    Console.ReadLine();

            //    host.Close();
            //    Environment.Exit(0);
            //}


            // ContainerBuilder builder = new ContainerBuilder();
            //builder.RegisterType<Logger<Program>>().As<ILogger>();
            //builder.RegisterType<ContractsService>();

            //using (IContainer container = builder.Build())
            //{

            //    ServiceHost host = new ServiceHost(typeof(ContractsService));
            //    host.AddDependencyInjectionBehavior<ContractsService>(container);
            //    host.Open();
            //    Console.WriteLine("The host has been opened.");
            //    Console.ReadLine();
            //    host.Close();

            //}


            //using(IContainer container = Bootstrapper.RegisterContainerBuilder().Build())
            //{
            //    ServiceHost host = new ServiceHost(typeof(ContractsService));

            //    IComponentRegistration registration;
            //    if (!container.ComponentRegistry.TryGetRegistration
            //       (new TypedService(typeof(ISalesOrder)), out registration))
            //    {
            //        Console.WriteLine("The service contract has not been registered in the container.");
            //        Console.ReadLine();
            //        Environment.Exit(-1);
            //    }

            //    host.AddDependencyInjectionBehavior<ISalesOrder>(container);
            //    host.Open();
            //    Console.WriteLine("Host has started");
            //    Console.ReadLine();

            //    host.Close();
            //    Environment.Exit(0);
            //}


            //ServiceHost host = new ServiceHost(typeof(ContractsService));
            //host.Open();
            //Console.WriteLine("Host has started");
            //Console.ReadLine();
            //IUnityContainer container = new UnityContainer();
            //container.RegisterType<IMoriaHandlerService, MoriaHandlerService>(/*new HierarchicalLifetimeManager()*/);
            //container.RegisterType<ContractsService>(/*new InjectionConstructor(container.Resolve<IMoriaHandlerService>())*/);
            //var factory = new WcfServiceFactory();


            //// 2nd Create a URI to serve as the base address.
            //var address = new Uri("http://localhost:5000/test");
            ////var selfHost = factory.CreateServiceHost("", new Uri[] { address });
            //// 3rd Create a UnityServiceHost instance
            //using (var selfHost = new UnityServiceHost(container, typeof(ContractsService), address))
            //{



            //    //ServiceHost selfHost = new ServiceHost(typeof(ContractsService), address);

            //    selfHost.AddDefaultEndpoints();

            //    // Step 4: Enable metadata exchange.
            //    ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            //    smb.HttpGetEnabled = true;
            //    selfHost.Description.Behaviors.Add(smb);

            //    // Step 5: Start the service.
            //    selfHost.Open();
            //    Console.WriteLine("The service is ready.");

            //    // Close the ServiceHost to stop the service.
            //    Console.WriteLine("Press <Enter> to terminate the service.");
            //    Console.WriteLine();
            //    Console.ReadLine();
            //    selfHost.Close();
            //}

        }
    }
}
