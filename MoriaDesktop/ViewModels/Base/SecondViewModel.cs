using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;

namespace MoriaDesktop.ViewModels.Base;
public class SecondViewModel : ViewModelBase
{
    public SecondViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService) : base(logger, appStateService)
    {
    }
}
