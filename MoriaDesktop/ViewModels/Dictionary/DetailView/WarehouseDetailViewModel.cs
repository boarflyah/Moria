using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;

namespace MoriaDesktop.ViewModels.Dictionary.DetailView;
public class WarehouseDetailViewModel : ViewModelBase
{
    public WarehouseDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService) : base(logger, appStateService)
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

    string _WarehouseName;
    public string WarehouseName
    {
        get => _WarehouseName;
        set
        {
            _WarehouseName = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion
}
