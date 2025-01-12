﻿using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;

namespace MoriaDesktop.ViewModels.Dictionary.DetailView;

public class MotorGearDetailViewModel : ViewModelBase
{
    public MotorGearDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, INavigationService navigationService) : base(logger, appStateService, navigationService)
    {
    }

    # region properties

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

    string _Ratio;
    public string Ratio
    {
        get => _Ratio;
        set
        {
            _Ratio = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion
}
