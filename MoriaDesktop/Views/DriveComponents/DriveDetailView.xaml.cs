using System.Windows.Controls;
using System.Windows.Input;
using MoriaBaseServices;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.DriveComponents;
using MoriaDesktop.ViewModels.Products;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.ViewModels;
using MoriaModelsDo.Base.Enums;
using MoriaModelsDo.Models.DriveComponents;
using MoriaModelsDo.Models.DriveComponents.Relations;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.Views.DriveComponents;

public partial class DriveDetailView : Page, IViewModelContent
{
    readonly ILookupService _lookupService;

    public object GetViewModel() => DataContext;
    public DriveDetailView(DriveDetailViewModel vm, ILookupService lookupService)
    {
        InitializeComponent();
        DataContext = vm;
        _lookupService = lookupService;
    }

    private void QuantityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !(sender as TextBox).Text.IsNumber(e.Text);
    }

    private void QuantityTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space)
        {
            e.Handled = true;
        }
    }

    private async void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
    {
        if (e.Column.SortMemberPath.Contains(nameof(MotorGearToDriveDo.MotorGear)))
        {
            if (e.Row.DataContext is MotorGearToDriveDo related)
            {
                var motorGear = await _lookupService.ShowLookup<MotorGearDo>();
                if (motorGear != null)
                {
                    related.MotorGear = motorGear;
                    if (related.ChangeType != SystemChangeType.Added)
                        related.ChangeType = SystemChangeType.Modified;
                    (DataContext as BaseDetailViewModel).HasObjectChanged = true;
                }
            }
            e.Cancel = true;
        }
    }

    private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        await (DataContext as BaseDetailViewModel).Load();
    }

    private async void MotorLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var motor = await _lookupService.ShowLookup<MotorDo>();
        if (motor != null)
            (DataContext as DriveDetailViewModel).Motor = motor;
    }
}
