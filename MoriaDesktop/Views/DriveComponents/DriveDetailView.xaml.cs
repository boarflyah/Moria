using System.Windows.Controls;
using System.Windows.Input;
using MoriaDesktop.ViewModels.DriveComponents;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.DriveComponents;

public partial class DriveDetailView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;
    public DriveDetailView(DriveDetailViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }

    private void QuantityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !IsTextAllowed(e.Text);
    }

    private void QuantityTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space)
        {
            e.Handled = true;
        }
    }

    private static bool IsTextAllowed(string text)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(text, @"^[0-9]*(?:[\.\,][0-9]*)?$");
    }
}
