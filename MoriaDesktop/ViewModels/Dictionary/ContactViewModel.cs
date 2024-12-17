using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;

namespace MoriaDesktop.ViewModels.Dictionary;
public class ContactViewModel : ViewModelBase
{
    public ContactViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService) : base(logger, appStateService)
    {
    }

    #region properties

    string _Symbol;
    public string Symbol
    {
        get => _Symbol;
        set
        {
            _Symbol = value;
            RaisePropertyChanged(value);
        }
    }

    string _ShortName;
    public string ShortName
    {
        get => _ShortName;
        set
        {
            _ShortName = value;
            RaisePropertyChanged(value);
        }
    }

    string _LongName;
    public string LongName
    {
        get => _LongName;
        set
        {
            _LongName = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion

}
