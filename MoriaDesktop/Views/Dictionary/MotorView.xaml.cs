using System.Windows.Controls;
using System.Windows.Input;
using MoriaDesktop.ViewModels.Dictionary;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Dictionary;

public partial class MotorView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public MotorView( MotorViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;
    }
    private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !IsTextAllowed(e.Text);
    }

    private static bool IsTextAllowed(string text)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(text, @"^[0-9]*(?:[\.\,][0-9]*)?$");
    }

    private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space)
        {
            e.Handled = true;
        }
    }
}
