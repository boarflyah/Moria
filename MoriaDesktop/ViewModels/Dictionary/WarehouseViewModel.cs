using Microsoft.Extensions.Logging;
using MoriaDesktop.ViewModels.Base;
using MoriaModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoriaDesktop.ViewModels.Dictionary;
public class WarehouseViewModel : ViewModelBase
{
    public WarehouseViewModel(ILogger<ViewModelBase> logger) : base(logger)
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

    string _WarehouseName;
    public string WarehouseName
    {
        get => _WarehouseName;
        set
        {
            _WarehouseName = value;
            RaisePropertyChanged(nameof(WarehouseName));
        }
    }

    #endregion
}
