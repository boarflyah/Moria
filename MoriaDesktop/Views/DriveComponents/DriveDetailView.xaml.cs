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
        if (!(DataContext as BaseDetailViewModel).IsLocked)
        {
            if (e.Row.DataContext is MotorGearToDriveDo related)
            {

                if (e.Column.SortMemberPath.Contains(nameof(MotorGearToDriveDo.MotorGear)))
                {

                    var motorGear = await _lookupService.ShowLookup<MotorGearDo>();
                    if (motorGear != null)
                    {
                        related.MotorGear = motorGear;
                        if (related.ChangeType != SystemChangeType.Added)
                            related.ChangeType = SystemChangeType.Modified;
                        (DataContext as BaseDetailViewModel).HasObjectChanged = true;
                    }

                    e.Cancel = true;
                }
                else
                {
                    if (related.ChangeType != SystemChangeType.Added)
                        related.ChangeType = SystemChangeType.Modified;
                    (DataContext as BaseDetailViewModel).HasObjectChanged = true;
                }
            }
        }
        else
            e.Cancel = true;
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

    private async void VariatorLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var variator = await _lookupService.ShowLookup<VariatorDo>();
        if (variator != null)
            (DataContext as DriveDetailViewModel).Variator = variator;
    }

    private async void InverterLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var inverter = await _lookupService.ShowLookup<InverterDo>();
        if (inverter != null)
            (DataContext as DriveDetailViewModel).Inverter = inverter;
    }

    private async void PumpLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var pump = await _lookupService.ShowLookup<PumpDo>();
        if (pump != null)
            (DataContext as DriveDetailViewModel).Pump = pump;
    }

    private async void ExternalCoolingLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var externalCooling = await _lookupService.ShowLookup<ExternalCoolingDo>();
        if (externalCooling != null)
            (DataContext as DriveDetailViewModel).ExternalCooling = externalCooling;
    }

    private async void BrakeLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var brake = await _lookupService.ShowLookup<BrakeDo>();
        if (brake != null)
            (DataContext as DriveDetailViewModel).Brake = brake;
    }

    private async void SupplementLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var supplement = await _lookupService.ShowLookup<SupplementDo>();
        if (supplement != null)
            (DataContext as DriveDetailViewModel).Supplement = supplement;
    }
}
