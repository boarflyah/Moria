using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktop.ViewModels.Dictionary.ListView;

public sealed class MotorGearListViewModel : BaseListViewModel
{
    public MotorGearListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService) : base(logger, appStateService, navigationService)
    {
    }

    #region properties

    public ObservableCollection<MotorGearDo> MotorGears { get; set; }


    #endregion

    protected override Task LoadList() => throw new NotImplementedException();

    public override void OnRowSelected(object row)
    {
        throw new NotImplementedException();
    }

    protected override void New() => throw new NotImplementedException();
    
    protected override Task<bool> SendDeleteRequest() => throw new NotImplementedException();
}
