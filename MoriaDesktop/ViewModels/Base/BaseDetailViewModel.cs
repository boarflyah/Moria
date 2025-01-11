using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.ViewModels.Base;
public abstract class BaseDetailViewModel : ViewModelBase, INavigationAware
{
    #region fields

    protected int objectId;

    #endregion

    protected BaseDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService) : base(logger, appStateService)
    {
    }

    #region properties



    #endregion

    #region methods

    public abstract Task Load();

    public virtual void OnNavigatedTo(params object[] parameters)
    {
        if (parameters.Any() && parameters.First() is int id)
            objectId = id;
    }

    #endregion
}
