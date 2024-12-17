using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaDesktop.ViewModels.DriveComponents;
public class DriveDetailViewModel : ViewModelBase
{
    public DriveDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService) : base(logger, appStateService)
    {
        Title = "Napęd";

        Drive = new();
    }

    #region Properties

    DriveDo _Drive;
    public DriveDo Drive
    {
        get => _Drive;
        set
        {
            _Drive = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion
}
