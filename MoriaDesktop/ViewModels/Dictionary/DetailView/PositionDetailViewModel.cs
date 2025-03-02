using Microsoft.Extensions.Logging;
using MoriaDesktop.Attributes;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktop.ViewModels.Dictionary.DetailView;

public class PositionDetailViewModel : BaseDetailWithNestedListViewModel
{
    readonly IApiPositionService _positionService;

    public PositionDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, INavigationService navigationService, IApiLockService apiLockService, IApiPositionService positionService)
        : base(logger, appStateService, apiLockService, navigationService)
    {
        _positionService = positionService;
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

    public override Type GetModelType() => typeof(PositionDo);

    protected async override Task LoadObject()
    {
        Clear();

        var position = await ExecuteApiRequest(_positionService.GetPosition, _appStateService.LoggedUser.Username, objectId);
        if (position != null)
            Setup(position);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);

    }

    protected async override Task<bool> SaveNewObject()
    {
        var position = GetDo() as PositionDo;
        var newObject = await _positionService.CreatePosition(_appStateService.LoggedUser.Username, position);
        if (newObject != null)
        {
            objectId = newObject.Id;
            return true;
        }
        return false;
    }

    protected async override Task<bool> UpdateExistingObject()
    {
        var position = GetDo() as PositionDo;
        var updated = await _positionService.UpdatePosition(_appStateService.LoggedUser.Username, position);
        return updated != null;
    }

    #endregion

    #region nestedlistview methods

    protected override string GetObjectsListViewTitle() => "Uprawnienia";
    protected override bool CanNestedNew() => false;
    protected override bool CanNestedDelete() => false;

    #endregion

    #region methods

    public override void Clear()
    {
        Code = string.Empty;
        Name = string.Empty;
        LastModified = string.Empty;
    }
    void Setup(PositionDo position)
    {
        Code = position.Code;
        Name = position.Name;
        LastModified = position.LastModified;
        if (position.Permissions != null)
            foreach (var permission in position.Permissions)
                Objects.Add(permission);
    }

    public override BaseDo GetDo()
    {
        var position = new PositionDo()
        {
            Id = objectId,
            Code = this.Code,
            Name = this.Name,
            LastModified = _appStateService.LoggedUser.Username,
        };

        foreach (var permission in Objects.Where(x => x.ChangeType != MoriaModelsDo.Base.Enums.SystemChangeType.None).OfType<PermissionDo>())
            position.Permissions.Add(permission);

        return position;
    }

    #endregion

}
