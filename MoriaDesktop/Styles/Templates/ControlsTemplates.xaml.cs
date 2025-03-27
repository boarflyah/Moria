using System.Windows;
using System.Windows.Controls;

namespace MoriaDesktop.Styles.Templates;
public partial class ControlsTemplates : ResourceDictionary
{
    public ControlsTemplates()
    {
        this.InitializeComponent();
    }

    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox)
        {
            textBox.SelectAll();
        }
    }

    private void TextBox_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (sender is TextBox textBox && !textBox.IsKeyboardFocusWithin)
        {
            e.Handled = true;
            textBox.Focus();
        }
    }
}
