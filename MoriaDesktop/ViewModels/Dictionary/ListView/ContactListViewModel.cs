using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaDesktopServices.Services.API;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace MoriaDesktop.ViewModels.Dictionary.ListView;

public sealed class ContactListViewModel : BaseListViewModel
{
    readonly IApiContactService _contactService;
    public ContactListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiContactService apiContactService) : base(logger, appStateService, navigationService)
    {
        _contactService = apiContactService;

        Contacts = new();
        Title = "Kontakty";
    }

    #region properties

    public ObservableCollection<ContactDo> Contacts { get; set; }

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

    protected override void New() => _navigationService.NavigateTo(typeof(ContactDetailViewModel), false, null);
    protected async override Task<bool> SendDeleteRequest() => await ExecuteApiRequest(_contactService.DeleteContact, _appStateService.LoggedUser.Username, (Selected as ContactDo)?.Id ?? 0);
}
