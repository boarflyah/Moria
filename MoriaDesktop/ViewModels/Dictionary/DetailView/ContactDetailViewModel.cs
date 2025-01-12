using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;

namespace MoriaDesktop.ViewModels.Dictionary.DetailView;
public class ContactDetailViewModel : BaseDetailViewModel
{
    public ContactDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, INavigationService navigationService, IApiLockService apiLockService) : base(logger, appStateService, apiLockService, navigationService)
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

    string _ShortName;
    public string ShortName
    {
        get => _ShortName;
        set
        {
            _ShortName = value;
            RaisePropertyChanged(value);
        }
    }

    string _LongName;
    public string LongName
    {
        get => _LongName;
        set
        {
            _LongName = value;
            RaisePropertyChanged(value);
        }
    }

    public override Task Load()
    {
        throw new NotImplementedException();
    }

    protected override Type GetModelType()
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

    protected override bool CheckPropertyName(string propertyName)
    {
        throw new NotImplementedException();
    }

    #endregion

}
