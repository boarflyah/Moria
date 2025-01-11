using Microsoft.Extensions.Logging;
using MoriaDesktop.Args;
using MoriaDesktop.Services;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.ViewModels.Base;
public abstract class BaseDetailViewModel : ViewModelBase, INavigationAware
{
    #region fields

    protected int objectId;
    protected bool isNew;

    #endregion

    protected BaseDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService) : base(logger, appStateService)
    {
    }

    #region properties


    private bool _IsLocked;
    public bool IsLocked
    {
        get => _IsLocked;
        set
        {
            _IsLocked = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion

    #region events

    public event EventHandler<EventArgs> Loaded;

    #endregion

    #region methods

    public abstract Task Load();

    public virtual void OnNavigatedTo(params object[] parameters)
    {
        if (parameters.Any() && parameters.First() is int id)
        {
            IsLocked = true;
            objectId = id;
        }
        else
            isNew = true;
    }

    #endregion
}
