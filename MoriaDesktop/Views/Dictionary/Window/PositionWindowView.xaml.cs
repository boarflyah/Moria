
namespace MoriaDesktop.Views.Dictionary.Window;

public partial class PositionWindowView : System.Windows.Window
{
    public PositionWindowView()
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
}
