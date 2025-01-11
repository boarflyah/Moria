using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoriaDesktop.ViewModels.Dictionary.DetailView;
public class MotorDetailViewModel : ViewModelBase
{
    public MotorDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService) : base(logger, appStateService)
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

    decimal _Power;
    public decimal Power
    {
        get => _Power;
        set
        {
            _Power = value;
            RaisePropertyChanged(value);
        }
    }
    #endregion
}
