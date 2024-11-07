using System.ComponentModel;
using System.ServiceProcess;

namespace MoriaWCFService
{
    [RunInstaller(true)]
    public partial class ServiceInstaller : System.Configuration.Install.Installer
    {
        public ServiceInstaller()
        {
            InitializeComponent();

            ServiceProcessInstaller processInstaller = new ServiceProcessInstaller();
            System.ServiceProcess.ServiceInstaller serviceInstaller = new System.ServiceProcess.ServiceInstaller();

            // Ustawienia konta usługi
            processInstaller.Account = ServiceAccount.LocalSystem;

            // Ustawienia usługi
            serviceInstaller.ServiceName = "MoriaWCFService";
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.DelayedAutoStart = true;

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);

        }
    }
}
