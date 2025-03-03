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
public class ContactDetailViewModel : BaseDetailViewModel
{
    readonly IApiContactService _contactService;
    public ContactDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, INavigationService navigationService, IApiLockService apiLockService, IApiContactService contactService, IKeepAliveWorker keepAliveWorker) 
        : base(logger, appStateService, apiLockService, navigationService, keepAliveWorker)
    {
        _contactService = contactService;
    }

    #region properties

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

    PermissionDo _Permission_ShortName;
    public PermissionDo Permission_ShortName
    {
        get => _Permission_ShortName;
        set
        {
            _Permission_ShortName = value;
            RaisePropertyChanged(value);
        }
    }

    PermissionDo _Permission_LongName;
    public PermissionDo Permission_LongName
    {
        get => _Permission_LongName;
        set
        {
            _Permission_LongName = value;
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

    protected async override Task<bool> SaveNewObject()
    {
        var contact = GetDo() as ContactDo;
        var newObject = await _contactService.CreateContact(_appStateService.LoggedUser.Username, contact);
        if (newObject != null)
        {
            objectId = newObject.Id;
            return true;
        }
        return false;
    }

    protected async override Task<bool> UpdateExistingObject()
    {
        var contact = GetDo() as ContactDo;
        var updated = await _contactService.UpdateContact(_appStateService.LoggedUser.Username, contact);
        return updated != null;
    }

    public override void Clear()
    {
        Symbol = string.Empty;
        LongName = string.Empty;
        ShortName = string.Empty;
        LastModified = string.Empty;
    }
    void Setup(ContactDo contact)
    {
        Symbol = contact.Symbol;
        LongName = contact.LongName;
        ShortName = contact.ShortName;
        LastModified = contact.LastModified;
    }

    public override BaseDo GetDo()
        => new ContactDo()
        {
            LongName = this.LongName,
            ShortName = this.ShortName,
            Symbol = this.Symbol,
            LastModified = _appStateService.LoggedUser.Username
        };

}
