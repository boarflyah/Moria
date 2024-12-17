
using Microsoft.Extensions.Logging;
using MoriaDesktop.ViewModels.Base;

namespace MoriaDesktop.ViewModels.Dictionary;

public class PositionViewModel : ViewModelBase
{
    public PositionViewModel(ILogger<ViewModelBase> logger) : base(logger)
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
            RaisePropertyChanged(nameof(Code));
        }
    }

    string _Name;
    public string Name
    {
        get => _Name;
        set
        {
            _Name = value;
            RaisePropertyChanged(nameof(Name));
        }
    }

    #endregion
}
