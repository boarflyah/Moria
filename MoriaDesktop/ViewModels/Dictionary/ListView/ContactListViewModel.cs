using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;
using System.Collections.ObjectModel;

namespace MoriaDesktop.ViewModels.Dictionary.ListView;

public sealed class ContactListViewModel : BaseListViewModel
{
    public ContactListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService) : base(logger, appStateService, navigationService)
    {
    }

    #region properties

    public ObservableCollection<ContactDo> Contacts { get; set; }

    #endregion

    protected override Task LoadList() => throw new NotImplementedException();

    public override void OnRowSelected(object row)
    {
        throw new NotImplementedException();
    }

    protected override void New() => throw new NotImplementedException();
    protected override Task<bool> SendDeleteRequest() => throw new NotImplementedException();
}
