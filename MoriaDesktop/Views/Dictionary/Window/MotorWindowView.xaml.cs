using System.Windows.Input;

namespace MoriaDesktop.Views.Dictionary.Window;
public partial class MotorWindowView : System.Windows.Window
{
    public MotorWindowView()
    {
        InitializeComponent();
    }

    private void DragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
        {
            this.DragMove();
        }
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

