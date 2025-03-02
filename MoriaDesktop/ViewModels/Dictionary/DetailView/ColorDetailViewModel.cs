using Microsoft.Extensions.Logging;
using MoriaDesktop.Attributes;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.ViewModels.Dictionary.DetailView;

public class ColorDetailViewModel : BaseDetailViewModel
{
    readonly IApiColorService _colorService;
    public ColorDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, INavigationService navigationService, IApiLockService apiLockService, IApiColorService colorService) : base(logger, appStateService, apiLockService, navigationService)
    {
        _colorService = colorService;
    }

    #region properties

    string _Code;
    [ObjectChangedValidate]
    public string Code
    {
        get => _Code;
        set
        {
            _Code = value;
            RaisePropertyChanged(value);
        }
    }

    string _Name;
    [ObjectChangedValidate]
    [DefaultProperty]
    public string Name
    {
        get => _Name;
        set
        {
            _Name = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_Code;
    public PermissionDo Permission_Code
    {
        get => _Permission_Code;
        set
        {
            _Permission_Code = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_Name;
    public PermissionDo Permission_Name
    {
        get => _Permission_Name;
        set
        {
            _Permission_Name = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion

    public override Type GetModelType() => typeof(ColorDo);

    protected async override Task LoadObject()
    {
        Clear();

        var color = await ExecuteApiRequest(_colorService.GetColor, _appStateService.LoggedUser.Username, objectId);
        if (color != null)
            Setup(color);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    protected async override Task<bool> SaveNewObject()
    {
        var color = GetDo() as ColorDo;
        var newObject = await _colorService.CreateColor(_appStateService.LoggedUser.Username, color);
        if (newObject != null)
        {
            objectId = newObject.Id;
            return true;
        }
        return false;
    }

    protected async override Task<bool> UpdateExistingObject()
    {
        var color = GetDo() as ColorDo;
        var updated = await _colorService.UpdateColor(_appStateService.LoggedUser.Username, color);
        return updated != null;
    }

    public override void Clear()
    {
        Name = string.Empty;
        Code = string.Empty;
        LastModified = string.Empty;
    }

    void Setup(ColorDo color)
    {
        Name = color.Name;
        Code = color.Code;
        LastModified = color.LastModified;
    }

    public override BaseDo GetDo()
        => new ColorDo()
        {
            Code = this.Code,
            Name = this.Name,
            LastModified = _appStateService.LoggedUser.Username,
        };
}
