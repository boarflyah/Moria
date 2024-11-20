using System.Windows.Controls;
using MoriaDesktop.ViewModels.Base;

namespace MoriaDesktop.Views.Base;
public partial class LoginView : Page
{
    public LoginView(LoginViewModel viewModel) : this()
    {
        DataContext = viewModel;
    }

    public LoginView()
    {
        InitializeComponent();
    }
}
