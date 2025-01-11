using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;
using System.Collections.ObjectModel;

namespace MoriaDesktop.ViewModels.Dictionary.ListView;

public sealed class ContactListViewModel : BaseListViewModel
{
    public ContactListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService) : base(logger, appStateService)
    {
    }

    #region properties

    public ObservableCollection<ContactDo> Contacts { get; set; }

    #endregion

    public override Task OnLoaded()
    {
        throw new NotImplementedException();
    }

    public override void OnRowSelected(object row)
    {
        throw new NotImplementedException();
    }
}
