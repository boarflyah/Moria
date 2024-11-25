using System.Windows.Controls;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Base;
public partial class LoginView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public LoginView(LoginViewModel viewModel) : this()
    {
        DataContext = viewModel;
    }

    public LoginView()
    {
        InitializeComponent();
    }
}
