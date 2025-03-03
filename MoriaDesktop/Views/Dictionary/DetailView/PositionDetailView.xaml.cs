using System.Windows.Controls;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktopServices.Interfaces.ViewModels;
using MoriaModelsDo.Base;

namespace MoriaDesktop.Views.Dictionary;
public partial class PositionDetailView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public PositionDetailView(PositionDetailViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;
    }

    private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        await (DataContext as BaseDetailViewModel)!.Load();
    }

    private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
    {
        if (e.Column.SortMemberPath.Contains(nameof(PermissionDo.DisplayName)))
            e.Cancel = true;
        else
        {
            (e.Row.DataContext as BaseDo).ChangeType = MoriaModelsDo.Base.Enums.SystemChangeType.Modified;
            (DataContext as BaseDetailViewModel).HasObjectChanged = true;
        }
    }
}

