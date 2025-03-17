using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;

namespace MoriaDesktop.ViewModels.Base;

public class CalendarViewModel : BaseDetailViewModel
{
    public CalendarViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, INavigationService navigationService, IKeepAliveWorker keepAliveWorker) : base(logger, appStateService, apiLockService, navigationService, keepAliveWorker)
    {
    }

    private bool _TechnicalDrawingCompleted;
    [ObjectChangedValidate]
    public bool TechnicalDrawingCompleted
    {
        get => _TechnicalDrawingCompleted;
        set
        {
            _TechnicalDrawingCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _CuttingCompleted;
    [ObjectChangedValidate]
    public bool CuttingCompleted
    {
        get => _CuttingCompleted;
        set
        {
            _CuttingCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _MetalCliningCompleted;
    [ObjectChangedValidate]
    public bool MetalCliningCompleted
    {
        get => _MetalCliningCompleted;
        set
        {
            _MetalCliningCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _PaintingCompleted;
    [ObjectChangedValidate]
    public bool PaintingCompleted
    {
        get => _PaintingCompleted;
        set
        {
            _PaintingCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _ElectricaCabinetCompleted;
    [ObjectChangedValidate]
    public bool ElectricaCabinetCompleted
    {
        get => _ElectricaCabinetCompleted;
        set
        {
            _ElectricaCabinetCompleted = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _MachineAssembled;
    [ObjectChangedValidate]
    public bool MachineAssembled
    {
        get => _MachineAssembled;
        set
        {
            _MachineAssembled = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _MachineWiredAndTested;
    [ObjectChangedValidate]
    public bool MachineWiredAndTested
    {
        get => _MachineWiredAndTested;
        set
        {
            _MachineWiredAndTested = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _MachineReleased;
    [ObjectChangedValidate]
    public bool MachineReleased
    {
        get => _MachineReleased;
        set
        {
            _MachineReleased = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _TransportOrdered;
    [ObjectChangedValidate]
    public bool TransportOrdered
    {
        get => _TransportOrdered;
        set
        {
            _TransportOrdered = value;
            RaisePropertyChanged(value);
        }
    }

    private bool _ProductionOrderSymbol;
    [ObjectChangedValidate]
    public bool ProductionOrderSymbol
    {
        get => _ProductionOrderSymbol;
        set
        {
            _ProductionOrderSymbol = value;
            RaisePropertyChanged(value);
        }
    }


    public override void Clear()
    {
        throw new NotImplementedException();
    }

    public override BaseDo GetDo()
    {
        throw new NotImplementedException();
    }

    public override Type GetModelType()
    {
        throw new NotImplementedException();
    }

    protected override Task LoadObject()
    {
        throw new NotImplementedException();
    }

    protected override Task<bool> SaveNewObject()
    {
        throw new NotImplementedException();
    }

    protected override Task<bool> UpdateExistingObject()
    {
        throw new NotImplementedException();
    }

    public override Task Load()
    {
        ;
        return base.Load();
    }
}
