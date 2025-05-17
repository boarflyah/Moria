using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Attributes;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Dictionaries;

namespace MoriaDesktop.ViewModels.Dictionary.DetailView;

public class ElectricalCabinetDetailViewModel : BaseDetailViewModel
{
    readonly IApiElectricalCabinetService _cabinetService;
    public ElectricalCabinetDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, INavigationService navigationService, IKeepAliveWorker keepAliveWorker, IApiElectricalCabinetService cabinetService) : base(logger, appStateService, apiLockService, navigationService, keepAliveWorker)
    {
        _cabinetService = cabinetService;
    }

    string _Symbol;
    [ObjectChangedValidate]
    [DefaultProperty]
    public string Symbol
    {
        get => _Symbol;
        set
        {
            _Symbol = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_Symbol;
    public PermissionDo Permission_Symbol
    {
        get => _Permission_Symbol;
        set
        {
            _Permission_Symbol = value;
            RaisePropertyChanged(value);
        }
    }


    public override void Clear()
    {
        Symbol = string.Empty;
        LastModified = string.Empty;
    }

    public override BaseDo GetDo() => new ElectricalCabinetDo()
    {
        Id = objectId,
        Symbol = this.Symbol,
        LastModified = _appStateService.LoggedUser.Username,
    };

    public override Type GetModelType() => typeof(ElectricalCabinetDo);

    protected async override Task LoadObject()
    {
        Clear();

        var color = await ExecuteApiRequest(_cabinetService.GetElectricalCabinet, _appStateService.LoggedUser.Username, objectId);
        if (color != null)
            Setup(color);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    protected async override Task<bool> SaveNewObject()
    {
        var cabinet = GetDo() as ElectricalCabinetDo;
        var newObject = await _cabinetService.CreateElectricalCabinet(_appStateService.LoggedUser.Username, cabinet);
        if (newObject != null)
        {
            objectId = newObject.Id;
            return true;
        }
        return false;
    }

    protected async override Task<bool> UpdateExistingObject()
    {
        var cabinet = GetDo() as ElectricalCabinetDo;
        var updated = await _cabinetService.UpdateElectricalCabinet(_appStateService.LoggedUser.Username, cabinet);
        return updated != null;
    }
    void Setup(ElectricalCabinetDo cabinet)
    {
        Symbol = cabinet.Symbol;
        LastModified = cabinet.LastModified;
    }
}
