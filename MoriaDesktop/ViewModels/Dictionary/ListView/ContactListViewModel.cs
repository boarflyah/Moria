using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaDesktopServices.Services.API;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace MoriaDesktop.ViewModels.Dictionary.ListView;

public sealed class ContactListViewModel : BaseListViewModel
{
    readonly IApiContactService _contactService;
    public ContactListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiContactService apiContactService, IListViewService listViewService) : base(logger, appStateService, navigationService, listViewService)
    {
        _contactService = apiContactService;

        Contacts = new();
        Title = "Kontakty";
    }

    #region properties

    public ObservableCollection<ContactDo> Contacts { get; set; }

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

    protected async override Task LoadList()
    {
        Contacts.Clear();

        var contacts = await ExecuteApiRequest(_contactService.GetContacts, _appStateService.LoggedUser.Username);
        if (contacts != null)
        {
            foreach (var contact in contacts)
                Contacts.Add(contact);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    public override void OnRowSelected(object row)
    {
        if (row is ContactDo cdo)
            _navigationService.NavigateTo(typeof(ContactDetailViewModel), false, cdo.Id);
    }

    protected async override Task Search()
    {
        if (SearchText != null)
        {
            try
            {
                var result = await ExecuteApiRequest(_listViewService.Search<ContactDo>, _appStateService.LoggedUser.Username, SearchText);
                if (result != null)
                {
                    Contacts.Clear();
                    foreach (var item in result)
                    {
                        Contacts.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Error, $"Brak danych do wczytania. {ex.Message}", true);
            }
        }
        else
            await LoadList();
    }

    protected override void New() => _navigationService.NavigateTo(typeof(ContactDetailViewModel), false, null);
    protected async override Task<bool> SendDeleteRequest() => await ExecuteApiRequest(_contactService.DeleteContact, _appStateService.LoggedUser.Username, (Selected as ContactDo)?.Id ?? 0);
}
