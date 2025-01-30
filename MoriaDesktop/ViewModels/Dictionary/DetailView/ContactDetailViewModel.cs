using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktop.ViewModels.Dictionary.DetailView;
public class ContactDetailViewModel : BaseDetailViewModel
{
    readonly IApiContactService _contactService;
    public ContactDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, INavigationService navigationService, IApiLockService apiLockService, IApiContactService contactService) : base(logger, appStateService, apiLockService, navigationService)
    {
        _contactService = contactService;
    }

    #region properties

    string _Symbol;
    [ObjectChangedValidate]
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
    [ObjectChangedValidate]
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
    [ObjectChangedValidate]
    public string LongName
    {
        get => _LongName;
        set
        {
            _LongName = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion

    public override Type GetModelType() => typeof(ContactDo);

    protected async override Task LoadObject()
    {
        Clear();

        var contact = await ExecuteApiRequest(_contactService.GetContact, _appStateService.LoggedUser.Username, objectId);
        if (contact != null)
            Setup(contact);
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    protected async override Task<bool> SaveNewObject() => true;

    protected async override Task<bool> UpdateExistingObject() => true;

    public override void Clear()
    {
        Symbol = string.Empty;
        LongName = string.Empty;
        ShortName = string.Empty;
    }
    void Setup(ContactDo contact)
    {
        Symbol = contact.Symbol;
        LongName = contact.LongName;
        ShortName = contact.ShortName;
    }

    public override BaseDo GetDo()
        => new ContactDo()
        {
            LongName = this.LongName,
            ShortName = this.ShortName,
            Symbol = this.Symbol
        };

}
