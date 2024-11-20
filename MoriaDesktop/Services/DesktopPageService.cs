using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.Views.Base;
using MoriaDesktopServices.Services;

namespace MoriaDesktop.Services;
public class DesktopPageService: PageService
{
    public DesktopPageService()
    {
        Register(typeof(LoginViewModel), typeof(LoginView));
        Register(typeof(SecondViewModel), typeof(SecondView));

    }
}
