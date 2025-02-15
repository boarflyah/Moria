using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using System.Windows;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktop.Services.Interfaces;

namespace MoriaDesktop.Views.Dictionary.Window;

public partial class SteelKindWindowView : System.Windows.Window, IDetailedWindow
{
    private SteelKindDetailViewModel detailViewModel;
    public SteelKindWindowView(SteelKindDetailViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;
        detailViewModel = viewModel;
    }

    #region BaseWindowFunctionality

    private void DragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
        {
            this.DragMove();
        }
    }

    private void WinowLoaed(object sender, RoutedEventArgs e)
    {
        var exitButton = (Button)this.Template.FindName("ExitButton", this);
        if (exitButton != null)
        {
            exitButton.Click -= ExitButton_Click;
            exitButton.Click += ExitButton_Click;
        }

        var saveAndCloseButton = (Button)this.Template.FindName("SaveAndCloseButton", this);
        if (saveAndCloseButton != null)
        {
            saveAndCloseButton.Click -= SaveAndCloseButton_Click;
            saveAndCloseButton.Click += SaveAndCloseButton_Click;
        }
    }

    private void MinimalizeButton_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        Cancelled = true;
        this.Hide();
    }

    private void SaveAndCloseButton_Click(object sender, RoutedEventArgs e)
    {
        Hide();
    }

    #endregion

    #region IDetailedWindow implementation

    public bool Cancelled
    {
        get; private set;
    }

    #endregion
}
