using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Models.Dictionaries;

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
    public string Name
    {
        get => _Name;
        set
        {
            _Name = value;
            RaisePropertyChanged(value);
        }
    }

    protected override Type GetModelType() => typeof(ColorDo);

    protected async override Task LoadObject()
    {
        Clear();

        var color = await ExecuteApiRequest(_colorService.GetColor, _appStateService.LoggedUser.Username, objectId);
        if (color != null)
            Setup(color);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    protected async override Task<bool> SaveNewObject() => true;

    protected async override Task<bool> UpdateExistingObject() => true;

    #endregion

    void Clear()
    {
        Name = string.Empty;
        Code = string.Empty;
    }
    void Setup(ColorDo color)
    {
        Name = color.Name;
        Code = color.Code;
    }
}
