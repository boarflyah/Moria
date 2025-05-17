using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using MoriaBaseModels.Models;
using MoriaBaseServices;
using MoriaDesktop.Services;
using MoriaDesktopServices.Interfaces;
using MoriaModelsDo.Models.Base;

namespace MoriaDesktop.ViewModels.Base;
public abstract class BaseListViewModel : ViewModelBase
{
    protected readonly IApiListViewService _listViewService;
    protected BaseListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService, IApiListViewService listViewService) : base(logger, appStateService, navigationService)
    {
        _listViewService = listViewService;
        NewCommand = new RelayCommand(New);
        DeleteCommand = new AsyncRelayCommand(Delete, CanDelete);
        RefreshCommand = new AsyncRelayCommand(Refresh);
        SearchCommand = new AsyncRelayCommand(Search);
    }

    #region properties


    private object _Selected;
    public object Selected
    {
        get => _Selected;
        set
        {
            _Selected = value;
            RaisePropertyChanged(value);
            DeleteCommand.NotifyCanExecuteChanged();
        }
    }

    private string _searchText;
    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged(nameof(SearchText));
        }
    }

    #endregion

    #region Commands

    public ICommand NewCommand
    {
        get;
    }
    public AsyncRelayCommand DeleteCommand
    {
        get;
    }
    public AsyncRelayCommand RefreshCommand
    {
        get;
    }

    public AsyncRelayCommand SearchCommand
    {
        get;
    }

    /// <summary>
    /// Implementation - navigate to object's detailview without any parameters, do not clear navigation
    /// </summary>
    protected abstract void New();


    protected abstract Task Search();

    protected async virtual Task Delete()
    {
        try
        {
            _appStateService.SetupLoading(true);

            var deleted = await SendDeleteRequest();
            if (deleted)
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Success, "Usunięto obiekt", true);
            else
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Nie udało się usunąć obiektu", true);

            await OnLoaded();
        }
        catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.ReAuthorizationCancelled)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Anulowano ponowną autoryzację", true);
        }
        catch (MoriaApiException apiException)
        {
            if (apiException.Reason == MoriaApiExceptionReason.ObjectIsLocked)
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, $"Obiekt jest w edycji przez innego użytkownika: {apiException.AdditionalMessage}", true);
            else
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Nieznany błąd", true);
        }
        catch (Exception ex)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Error, ex.Message, true);
        }
        finally
        {
            _appStateService.SetupLoading();
        }
    }

    protected virtual bool CanDelete() => Selected != null;

    protected async virtual Task Refresh()
    {
        await OnLoaded();
    }

    #endregion

    #region methods

    public async virtual Task OnLoaded()
    {
        try
        {
            _appStateService.SetupLoading(true);

            await LoadList();
        }
        catch (MoriaAppException mae) when (mae.Reason == MoriaAppExceptionReason.ReAuthorizationCancelled)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Anulowano ponowną autoryzację", true);
        }
        catch (Exception ex)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Error, ex.Message, true);
        }
        finally
        {
            _appStateService.SetupLoading();
        }
    }

    /// <summary>
    /// Implementation - send api request to get list of objects and add them to observablecollection
    /// </summary>
    /// <returns></returns>
    protected abstract Task LoadList();

    /// <summary>
    /// Implementation - navigate to object's detailview passing object's id as parameter, do not clear navigation
    /// </summary>
    /// <param name="row">double clicked row on grid</param>
    public abstract void OnRowSelected(object row);

    /// <summary>
    /// Implementation - send delete request for the <see cref="Selected"/> object
    /// </summary>
    /// <returns></returns>
    protected abstract Task<bool> SendDeleteRequest();

    public async Task UpdateListViewSetup(IList<ListViewColumnProvider> columns)
    {
        try
        {
            if ( _appStateService.LoggedUser != null)
                await _listViewService.CreateUpdateListViewSetup(_appStateService.LoggedUser.Username, GetType().Name, columns);
        }
        catch (Exception e)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, e.Message, true);
        }
    }

    public async Task<ListViewSetupDo> GetListViewSetup()
    {
        try
        {
            return await _listViewService.GetListViewSetup(_appStateService.LoggedUser.Username, GetType().Name);
        }
        catch (Exception e)
        {
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Warning, e.Message, true);
        }

        return null;
    }

    #endregion
}
