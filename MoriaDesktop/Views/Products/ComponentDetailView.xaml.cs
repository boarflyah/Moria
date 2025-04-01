using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MoriaBaseServices;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.DriveComponents;
using MoriaDesktop.ViewModels.Products;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.ViewModels;
using MoriaModelsDo.Base.Enums;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;
using MoriaModelsDo.Models.DriveComponents.Relations;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.Views.Products;

public partial class ComponentDetailView : Page, IViewModelContent
{
    readonly ILookupService _lookupService;

    public object GetViewModel() => DataContext;

    public ComponentDetailView(ComponentDetailViewModel vm, ILookupService lookupService)
    {
        InitializeComponent();
        DataContext = vm;
        _lookupService = lookupService;
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await (DataContext as BaseDetailViewModel).Load();
    }

    private async void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
    {
        if (!(DataContext as BaseDetailViewModel).IsLocked)
        {
            if (e.Column.SortMemberPath.Contains(nameof(DriveToComponentDo.Drive)))
            {
                if (e.Row.DataContext is DriveToComponentDo related)
                {
                    var drive = await _lookupService.ShowLookup<DriveDo>(false);
                    if (drive != null)
                    {
                        related.Drive = drive;
                        if (related.ChangeType != SystemChangeType.Added)
                            related.ChangeType = SystemChangeType.Modified;
                        (DataContext as BaseDetailViewModel).HasObjectChanged = true;
                    }
                }
                e.Cancel = true;
            }
            else
                (DataContext as BaseDetailViewModel).HasObjectChanged = true;
        }
        else
            e.Cancel = true;
    }

    private void QuantityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !(sender as TextBox).Text.IsNumber(e.Text);
    }

    private async void ColorLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var color = await _lookupService.ShowLookup<ColorDo>();
        if (color != null)
            (DataContext as ComponentDetailViewModel).ComponentColor = color;
    }

    private async void ProductLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var product = await _lookupService.ShowLookup<ProductDo>(false);
        if (product != null)
            (DataContext as ComponentDetailViewModel).ComponentProduct = product;
    }
}
