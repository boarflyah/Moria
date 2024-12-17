using MoriaDesktop.ViewModels.Dictionary;
using System.Windows.Controls;
namespace MoriaDesktop.Views.Dictionary;
public partial class WarehouseView : Page
{
    public WarehouseView(WarehouseViewModel vm)
    {
        InitializeComponent();
        this.DataContext = vm;
    }
}
