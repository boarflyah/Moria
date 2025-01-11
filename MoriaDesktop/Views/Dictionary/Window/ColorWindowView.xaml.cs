using CommunityToolkit.Mvvm.Input;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using System.Windows;
using System.Windows.Input;

namespace MoriaDesktop.Views.Dictionary.Window;
public partial class ColorWindowView : System.Windows.Window 
{
    public ICommand CloseCommand { get; private set; }
    public ColorWindowView(ColorDetailViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;
        CloseCommand = new RelayCommand(CloseWindow);
    }
    private void CloseWindow()
    {
        this.Close();
    }
    private void DragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
        {
            this.DragMove();
        }
    }

    private void MinimizeWindow(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    private void CloseWindow(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

}
