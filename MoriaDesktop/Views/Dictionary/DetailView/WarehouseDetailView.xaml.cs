using MoriaDesktop.ViewModels.Dictionary.DetailView;
using System.Windows.Controls;
namespace MoriaDesktop.Views.Dictionary;
public partial class WarehouseDetailView : Page
{
    public WarehouseDetailView(WarehouseDetailViewModel vm)
    {
        InitializeComponent();
        this.DataContext = vm;
    }
}
