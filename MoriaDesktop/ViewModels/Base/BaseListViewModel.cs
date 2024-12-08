using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;

namespace MoriaDesktop.ViewModels.Base;
public abstract class BaseListViewModel : ViewModelBase
{
    protected BaseListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService) : base(logger, appStateService)
    {
    }

    #region properties

    #endregion

    #region commands

    #endregion

    #region methods

    public abstract Task OnLoaded();

    public abstract void OnRowSelected(object row);

    #endregion
}
