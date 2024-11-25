using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Commands;
using MoriaDesktop.Models;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.ViewModels.Base;

public class MainWindowViewModel : ViewModelBase
{
    #region DI properties

    readonly INavigationService _navigationService;
    readonly IApiTokenService _tokenService;

    #endregion

    public MainWindowViewModel(ILogger<MainWindowViewModel> logger, INavigationService navigationService, IApiTokenService tokenService) : base(logger)
    {
        _navigationService = navigationService;
        _tokenService = tokenService;

        _navigationService.OnNavigated += _navigationService_OnNavigated;

        GoBackCommand = new(GoBack, CanGoBack);
        SetFullScreenCommand = new(SetFullScreen, CanSetFullScreen);
        SetWindowCommand = new(SetWindow, CanSetWindow);
        CloseAppCommand = new(CloseApp, CanCloseApp);

        IsFullScreen = true;

        #region mockup

        Navigation = new();
        NavigationItem node1 = new()
        {
            Title = "CRM",
            Items = new()
        };

        NavigationItem node2 = new()
        {
            Title = "Produkcja",
            Items = new()
        };

        Navigation.Add(node1);
        Navigation.Add(node2);

        node2.Items.Add(new()
        {
            Title = "Zamówienia",
        });
        node2.Items.Add(new()
        {
            Title = "Napędy"
        });

        node1.Items.Add(new()
        {
            Title = "Pracownicy"
        });
        node1.Items.Add(new()
        {
            Title = "Podmioty"
        });

        #endregion
    }

    #region properties

    public ObservableCollection<NavigationItem> Navigation { get; set; }

    NavigationItem _SelectedItem;
    public NavigationItem SelectedItem
    {
        get => _SelectedItem;
        set
        {
            _SelectedItem = value;
            if (_SelectedItem != null)
                _SelectedItem.IsSelected = true;
            RaisePropertyChanged(nameof(SelectedItem), value);
        }
    }

    bool _IsFullScreen;
    public bool IsFullScreen
    {
        get => _IsFullScreen;
        set
        {
            _IsFullScreen = value;
            SetFullScreenCommand?.RaiseCanExecuteChanged();
            SetWindowCommand?.RaiseCanExecuteChanged();
            RaisePropertyChanged(nameof(IsFullScreen), value);
        }
    }

    #endregion

    #region commands

    public BaseCommand GoBackCommand { get; init; }

    public BaseCommand SetFullScreenCommand { get; init; }

    public BaseCommand SetWindowCommand  { get; init; }

    public BaseCommand CloseAppCommand { get; init; }

    #endregion

    #region commands methods

    void GoBack()
    {
        _navigationService.GoBack();
    }

    bool CanGoBack() => _navigationService.CanGoBack;

    void SetFullScreen()
    {
        IsFullScreen = true;
    }

    bool CanSetFullScreen() => !IsFullScreen;

    void SetWindow()
    {
        IsFullScreen = false;
    }

    bool CanSetWindow() => IsFullScreen;

    void CloseApp()
    {
    
    }

    bool CanCloseApp() => true;

    #endregion

    #region methods

    public void OnNavigationSelectionChanged(object obj)
    {
        foreach (var child in Navigation)
            RestartSelection(child);
        if (obj is NavigationItem ni && !ni.Items.Any())
            SelectedItem = ni;
        else
            SelectedItem = null;
    }

    bool RestartSelection(NavigationItem ni)
    {
        ni.IsSelected = false;
        foreach (var child in ni.Items)
        {
            RestartSelection(child);
        }
        return true;
    }

    public void NavigateToFirstView()
    {
        _navigationService.NavigateTo<LoginViewModel>(true);
    }

    #endregion

    #region events

    private void _navigationService_OnNavigated(object? sender, MoriaBaseServices.Args.OnNavigatedEventArgs e)
    {
        if (e.Content is IViewModelContent content && content.GetViewModel() is ViewModelBase vmb)
        {
            vmb.OnReAuthorizationNeeded -= OnReAuthorizationNeeded;
            vmb.OnReAuthorizationNeeded += OnReAuthorizationNeeded;
        }
        GoBackCommand?.RaiseCanExecuteChanged();
    }

    private async void OnReAuthorizationNeeded(object? sender, Args.InvokeViewEventArgs e)
    {
        //TODO show new window or popup with username/password fields to login again
        //or maybe password only with username control disabled
        var employee = await _tokenService.GetUserWithToken("123", "abc");

        e.CompletionSource.SetResult(employee != null && !string.IsNullOrWhiteSpace(employee.Token) && employee.ValidTo > DateTime.Now);
    }

    #endregion
}
