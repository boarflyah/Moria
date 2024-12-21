using MoriaDesktop.ViewModels.Dictionary;
using MoriaDesktopServices.Interfaces.ViewModels;
using System.Windows.Controls;
namespace MoriaDesktop.Views.Dictionary;
public partial class WarehouseView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public WarehouseView(WarehouseViewModel vm)
    {
        InitializeComponent();
        this.DataContext = vm;
    }
}
