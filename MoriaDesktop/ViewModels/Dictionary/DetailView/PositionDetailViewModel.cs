
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;

namespace MoriaDesktop.ViewModels.Dictionary.DetailView;

public class PositionDetailViewModel : ViewModelBase
{
    public PositionDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService) : base(logger, appStateService)
    {
    }

    #region properties

    string _Code;
    public string Code
    {
        get => _Code;
        set
        {
            _Code = value;
            RaisePropertyChanged(value);
        }
    }

    string _Name;
    public string Name
    {
        get => _Name;
        set
        {
            _Name = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion
}
