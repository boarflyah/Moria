
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;

namespace MoriaDesktop.ViewModels.Dictionary.DetailView;

public class PositionDetailViewModel : BaseDetailViewModel
{
    readonly IApiPositionService _positionService;
    public PositionDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, INavigationService navigationService, IApiLockService apiLockService, IApiPositionService positionService) : base(logger, appStateService, apiLockService, navigationService)
    {
        _positionService = positionService;
    }

    #region properties

    string _Code;
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
    public string Name
    {
        get => _Name;
        set
        {
            _Name = value;
            RaisePropertyChanged(value);
        }
    }


    protected override Type GetModelType() => typeof(PositionDo);

    protected async override Task LoadObject()
    {
        Clear();

        var position = await ExecuteApiRequest(_positionService.GetPosition, _appStateService.LoggedUser.Username, objectId);
        if (position != null)
            Setup(position);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);

    }

    protected async override Task<bool> SaveNewObject() => true;

    protected async override Task<bool> UpdateExistingObject() => true;

    protected override bool CheckPropertyName(string propertyName) =>
        propertyName.Equals(nameof(Code)) || propertyName.Equals(nameof(Name));

    #endregion

    void Clear()
    {
        Code = string.Empty;
        Name = string.Empty;
    }
    void Setup(PositionDo position)
    {
        Code = position.Code;
        Name = position.Name;
    }
}
