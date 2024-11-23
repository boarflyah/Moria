using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using MoriaBaseServices.Services;
using MoriaDesktop.Commands;
using MoriaDesktopServices.Interfaces;

namespace MoriaDesktop.ViewModels.Base;
public class LoginViewModel : ViewModelBase
{
    #region services

    readonly INavigationService _navigationService;
    readonly ApiRequestService _apiRequestService;

    #endregion

    public LoginViewModel(ILogger<ViewModelBase> logger, INavigationService navigationService, ApiRequestService apiRequestService) : base(logger)
    {
        _navigationService = navigationService;
        _apiRequestService = apiRequestService;

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
        var token = await _apiRequestService.Get("http", "192.168.0.59", 5000, WebAPIEndpointsProvider.GetTestPath, null);

        //await Task.Delay(2000);
        //Navigate();
    }

    #endregion

    #region methods

    public void Navigate()
    {
        _navigationService.NavigateTo<SecondViewModel>(false);
    }

    #endregion
}
