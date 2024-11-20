using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Commands;
using MoriaDesktopServices.Interfaces;

namespace MoriaDesktop.ViewModels.Base;
public class LoginViewModel : ViewModelBase
{
    #region services

    readonly INavigationService _navigationService;

    #endregion

    public LoginViewModel(ILogger<ViewModelBase> logger, INavigationService navigationService) : base(logger)
    {
        _navigationService = navigationService;

        LoginCommand = new(Login);
        TestOid = 10;
    }

    #region properties

    int _TestOid;
    public int TestOid
    {
        get => _TestOid;
        set
        {
            _TestOid = value;
            RaisePropertyChanged(nameof(TestOid), value);
        }
    }

    #endregion

    #region commands

    public AsyncRelayCommand LoginCommand
    {
        get;
        init;
    }

    #endregion

    #region commands methods

    async Task Login()
    {
        await Task.Delay(2000);
        Navigate();
    }

    #endregion

    #region methods

    public void Navigate()
    {
        _navigationService.NavigateTo<SecondViewModel>(false);
    }

    #endregion
}
