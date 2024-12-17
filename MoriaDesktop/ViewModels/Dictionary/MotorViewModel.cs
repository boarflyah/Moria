using Microsoft.Extensions.Logging;
using MoriaDesktop.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoriaDesktop.ViewModels.Dictionary;
public class MotorViewModel : ViewModelBase
{
    public MotorViewModel(ILogger<ViewModelBase> logger) : base(logger)
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

    decimal _Power;
    public decimal Power
    {
        get => _Power;
        set
        {
            _Power = value;
            RaisePropertyChanged(nameof(Power));
        }
    }
    #endregion
}
