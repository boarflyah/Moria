using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Contacts;
using MoriaDesktop.Views.Base;
using MoriaDesktop.Views.Contacts;
using MoriaDesktopServices.Services;

namespace MoriaDesktop.Services;
public class DesktopPageService: PageService
{
    public DesktopPageService()
    {
        Register(typeof(LoginViewModel), typeof(LoginView));
        Register(typeof(SecondViewModel), typeof(SecondView));
        Register(typeof(EmployeeListViewModel), typeof(EmployeeListView));
    }
}
