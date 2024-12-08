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

    #endregion

}
