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
        UsernameTextBox.Focus();
    }

    private async void LoginButton_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        await (DataContext as LoginViewModel)!.Login(UsernameTextBox.Text, PasswordBox.Password);
    }

    private async void Page_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if(e.Key == System.Windows.Input.Key.Enter && UsernameTextBox.Text != string.Empty)
            await (DataContext as LoginViewModel)!.Login(UsernameTextBox.Text, PasswordBox.Password);
    }
}
