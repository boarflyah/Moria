using System.Windows.Xps;
using MoriaBaseServices;
using MoriaDesktop.Args;
using MoriaDesktop.Models.Enums;
using MoriaDesktop.ViewModels.Base;
using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktop.Services;

public class AppStateService
{
    MainWindowViewModel _mainViewModel;

    public AppStateService()
    {
    }

    #region properties

    public EmployeeDo LoggedUser
    {
        get; set;
    }

    public Type CurrentDetailViewObjectType
    {
        get; set;
    }

    public int CurrentDetailViewObjectId
    {
        get; set;
    }

    #endregion

    #region Methods

    public void SetupInfo(SystemInfoStatus status, string text, bool isVisible)
    {
        _mainViewModel.SetupInfo(status, text, isVisible);
    }

    public void SetupLoading(bool visible = false, string text = "Wczytywanie...")
    {
        _mainViewModel.SetupLoading(visible, text);
    }

    public void SetupTitle(string title)
    {
        _mainViewModel.PageTitle = title;
    }

    public void OnLoggedIn(EmployeeDo employee)
    {
        _mainViewModel.IsLoggedIn = true;
        _mainViewModel.Username = employee.Username;
        LoggedUser = employee;
    }

    public void OnLoggingOut()
    {
        _mainViewModel.IsLoggedIn = false;
        _mainViewModel.Username = string.Empty;
        LoggedUser = null;
    }

    public void SetMainViewModel(MainWindowViewModel mvm)
    {
        _mainViewModel = mvm;
    }

    public async Task<bool?> ConfirmAsync(string confirmationMessage)
    {
        var args = new ConfirmationEventArgs(confirmationMessage);
        return await _mainViewModel.ConfirmationRequired(this, args);
    }

    public void HandleMoriaApiException(MoriaApiException mae)
    {
        switch (mae.Reason)
        {
            case MoriaApiExceptionReason.ObjectNotFound:
                SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Nie znaleziono obiektu", true);
                break;
            case MoriaApiExceptionReason.ObjectIsLocked:
                SetupInfo(Models.Enums.SystemInfoStatus.Warning, $"Obiekt jest w edycji przez innego użytkownika: {mae.AdditionalMessage}", true);
                break;
            case MoriaApiExceptionReason.ValueIsNotUnique:
                SetupInfo(Models.Enums.SystemInfoStatus.Warning, "Wartość musi być unikalna", true);
                break;
            default:
                SetupInfo(Models.Enums.SystemInfoStatus.Warning, mae.Reason.ToString(), true);
                break;
        }
    }

    #endregion

}
