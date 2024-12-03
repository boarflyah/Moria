using MoriaDesktop.Models.Enums;
using MoriaDesktop.ViewModels.Base;
using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktop.Services;

public class AppStateService
{
    readonly MainWindowViewModel _mainViewModel;

    public AppStateService(MainWindowViewModel mwvm)
    {
        _mainViewModel = mwvm;
    }

    public EmployeeDo LoggedUser
    {
        get; set;
    }

    public void SetupInfo(SystemInfoStatus status, string text, bool isVisible)
    {
        _mainViewModel.SetupInfo(status, text, isVisible);
    }

    public void SetupLoading(bool visible = false, string text = "Wczytywanie...")
    {
        _mainViewModel.SetupLoading(visible, text);
    }
}
