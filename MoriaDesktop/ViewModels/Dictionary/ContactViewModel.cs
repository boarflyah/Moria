using Microsoft.Extensions.Logging;
using MoriaDesktop.ViewModels.Base;

namespace MoriaDesktop.ViewModels.Dictionary;
public class ContactViewModel : ViewModelBase
{
    public ContactViewModel(ILogger<ViewModelBase> logger) : base(logger)
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
            RaisePropertyChanged(nameof(Symbol));
        }
    }

    string _ShortName;
    public string ShortName
    {
        get => _ShortName;
        set
        {
            _ShortName = value;
            RaisePropertyChanged(nameof(ShortName));
        }
    }

    string _LongName;
    public string LongName
    {
        get => _LongName;
        set
        {
            _LongName = value;
            RaisePropertyChanged(nameof(LongName));
        }
    }

    #endregion

}
