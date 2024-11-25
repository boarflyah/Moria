using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using MoriaBaseServices.Services;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaDesktopServices.Services.API;
using MoriaModelsDo.Models.Contacts;

namespace MoriaDesktop.ViewModels.Base;
public class LoginViewModel : ViewModelBase
{
    #region services

    readonly INavigationService _navigationService;
    readonly IApiTokenService _tokenService;

    readonly ApiTestService _testService;

    #endregion

    public LoginViewModel(ILogger<ViewModelBase> logger, INavigationService navigationService, IApiTokenService tokenService, ApiTestService testService) : base(logger)
    {
        _navigationService = navigationService;
        _tokenService = tokenService;
        _testService = testService;

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
        var result = await ExecuteApiRequest(_testService.Get);

        var employee = await ExecuteApiRequest(_tokenService.GetUserWithToken, "123", "abc");

        //var employee = await ExecuteApiRequest(() =>
        //{
        //    return _tokenService.GetUserWithToken("123", "abc");
        //});

        //var token = await _tokenService.GetUserWithToken("123", "abc");

        //var authorizationHeader = _apiRequestService.GetAuthorizationHeader(token.Token);
        //var testGet = await _apiRequestService.Get("http", "192.168.0.59", 5000, WebAPIEndpointsProvider.GetTestPath, new()
        //{
        //    { authorizationHeader.Key, authorizationHeader.Value }
        //});

    }

    #endregion

    #region methods

    public void Navigate()
    {
        _navigationService.NavigateTo<SecondViewModel>(false);
    }

    #endregion
}
