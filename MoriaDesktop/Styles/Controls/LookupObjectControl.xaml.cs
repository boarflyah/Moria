using System.Windows;
using System.Windows.Controls;

namespace MoriaDesktop.Styles.Controls;

public partial class LookupObjectControl : UserControl
{
    public LookupObjectControl()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty NameTextProperty =
        DependencyProperty.Register(
            nameof(NameText),
            typeof(string),
            typeof(LookupObjectControl),
            new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnNameTextChanged)));

    public string NameText
    {
        get => (string)GetValue(NameTextProperty);
        set => SetValue(NameTextProperty, value);
    }

    public static readonly DependencyProperty LookupObjectProperty =
    DependencyProperty.Register(
        nameof(LookupObject),
        typeof(object),
        typeof(LookupObjectControl),
        new PropertyMetadata(null));

    public object LookupObject
    {
        get => GetValue(LookupObjectProperty);
        set => SetValue(LookupObjectProperty, value);
    }

    public event EventHandler<EventArgs> OnLookupInvoked;

    private static void OnNameTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if(d is LookupObjectControl loc)
            loc.SymbolTextBox.Text = e.NewValue?.ToString();
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        LookupObject = null;
    }

    private void SymbolTextBox_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        OnLookupInvoked?.Invoke(this, EventArgs.Empty);
    }

    private void SymbolTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if(e.Key == System.Windows.Input.Key.Space || e.Key == System.Windows.Input.Key.Return)
            OnLookupInvoked?.Invoke(this, EventArgs.Empty);
    }
}
