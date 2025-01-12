using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktopServices.Interfaces;

namespace MoriaDesktop.ViewModels.Base;
public class SecondViewModel : ViewModelBase
{
    public SecondViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, INavigationService navigationService) : base(logger, appStateService, navigationService)
    {
    }
}
