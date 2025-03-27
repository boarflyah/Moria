using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MoriaBaseServices;
using MoriaDesktop.Services.Interfaces;
using MoriaDesktop.ViewModels.Dictionary.DetailView;

namespace MoriaDesktop.Views.Dictionary.Window;
public partial class MotorWindowView : System.Windows.Window, IDetailedWindow
{
    public MotorWindowView(MotorDetailViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    public bool Cancelled
    {
        get; private set;
    }

    private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !(sender as TextBox).Text.IsNumber(e.Text);
    }

    private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space)
        {
            e.Handled = true;
        }
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

    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        Cancelled = true;
        this.Close();
    }

    private void SaveAndCloseButton_Click(object sender, RoutedEventArgs e)
    {
        Hide();
    }

    #endregion
}

