using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Commands;
using MoriaDesktop.Models;
using MoriaDesktop.Views.Dictionary;
using MoriaDesktop.Models.Enums;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Contacts;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaDesktopServices.Interfaces.ViewModels;
using MoriaModelsDo.Base;
using MoriaDesktop.ViewModels.DriveComponents;
using MoriaDesktop.Views.Dictionary.DetailView;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktop.ViewModels.Products;
using MoriaDesktop.ViewModels.Dictionary.ListView;

namespace MoriaDesktop.ViewModels.Base;

public class MainWindowViewModel : BaseNotifyPropertyChanged
{
    #region DI properties

    readonly INavigationService _navigationService;
    readonly IApiTokenService _tokenService;
    readonly AppStateService _appStateService;
    readonly ILogger<MainWindowViewModel> _logger;

    #endregion

    public MainWindowViewModel(ILogger<MainWindowViewModel> logger, INavigationService navigationService, IApiTokenService tokenService, AppStateService appStateService) : base()
    {
        _navigationService = navigationService;
        _tokenService = tokenService;
        _appStateService = appStateService;
        _logger = logger;

        _navigationService.OnNavigated += _navigationService_OnNavigated;

        GoBackCommand = new(GoBack, CanGoBack);
        SetFullScreenCommand = new(SetFullScreen, CanSetFullScreen);
        SetWindowCommand = new(SetWindow, CanSetWindow);
        CloseAppCommand = new(CloseApp, CanCloseApp);
        CloseInfoCommand = new(CloseInfo);
        LogoutCommand = new(Logout);

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
            Title = "Słowniki",
            Items = new()
        };
        NavigationItem node4 = new()
        {
            Title = "Produkty",
            Items = new()
        };
        NavigationItem node3 = new()
        {
            Title = "Testy",
            Items = new()
        };

        Navigation.Add(node1);
        Navigation.Add(node2);
        Navigation.Add(node4);
        Navigation.Add(node3);

        node1.Items.Add(new()
        {
            Title = "Pracownicy",
            ViewModelType = typeof(EmployeeListViewModel),
        });
        node1.Items.Add(new()
        {
            Title = "Podmioty",
            ViewModelType = typeof(ContactListViewModel),
        });

        node2.Items.Add(new()
        {
            Title = "Kolory",
            ViewModelType = typeof(ColorListViewModel)
        });
        node2.Items.Add(new()
        {
            Title = "Magazyny",
            ViewModelType = typeof(WarehouseListViewModel),
        });
        node2.Items.Add(new()
        {
            Title = "Przekładnie",
            ViewModelType = typeof(MotorGearListViewModel),
        });
        node2.Items.Add(new()
        {
            Title = "Silniki",
            ViewModelType = typeof(MotorListViewModel),
        });
        node2.Items.Add(new()
        {
            Title = "Stanowiska",
            ViewModelType = typeof(PositionListViewModel),
        });
        node2.Items.Add(new()
        {
            Title = "Rodzaj stali",
            ViewModelType = typeof(SteelKindListViewModel),
        });

        node3.Items.Add(new()
        {
            Title = "Test",
            ViewModelType = typeof(ColorDetailViewModel),
        });

        node4.Items.Add(new()
        {
            Title = "Produkty",
            ViewModelType = typeof(ProductListViewModel)
        });
        node4.Items.Add(new()
        {
            Title = "Kategorie",
            ViewModelType = typeof(CategoryListViewModel)
        });


        #endregion
    }

    #region properties

    public ObservableCollection<NavigationItem> Navigation { get; set; }

    string _PageTitle;
    public string PageTitle
    {
        get => _PageTitle;
        set
        {
            _PageTitle = value;
            RaisePropertyChanged(value);
        }
    }

    NavigationItem _SelectedItem;
    public NavigationItem SelectedItem
    {
        get => _SelectedItem;
        set
        {
            _SelectedItem = value;
            if (_SelectedItem != null)
                _SelectedItem.IsSelected = true;
            RaisePropertyChanged(value);
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
            RaisePropertyChanged(value);
        }
    }

    bool _IsLoggedIn;
    public bool IsLoggedIn
    {
        get => _IsLoggedIn;
        set
        {
            _IsLoggedIn = value;
            RaisePropertyChanged(value);
        }
    }

    string _Username;
    public string Username
    {
        get => _Username;
        set
        {
            _Username = value;
            RaisePropertyChanged(value);
        }
    }

    #region infobar

    bool _IsInfoVisible;
    public bool IsInfoVisible
    {
        get => _IsInfoVisible;
        set
        {
            _IsInfoVisible = value;
            RaisePropertyChanged(value);
        }
    }

    SystemInfoStatus _InfoStatus;
    public SystemInfoStatus InfoStatus
    {
        get => _InfoStatus;
        set
        {
            _InfoStatus = value;
            RaisePropertyChanged(value);
        }
    }

    string _InfoText;
    public string InfoText
    {
        get => _InfoText;
        set
        {
            _InfoText = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion

    #region loading bar

    bool _IsLoadingVisible;
    public bool IsLoadingVisible
    {
        get => _IsLoadingVisible;
        set
        {
            _IsLoadingVisible = value;
            RaisePropertyChanged(value);
        }
    }

    string _LoadingText;
    public string LoadingText
    {
        get => _LoadingText;
        set
        {
            _LoadingText = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion

    #endregion

    #region commands

    public BaseCommand GoBackCommand { get; init; }
    public BaseCommand SetFullScreenCommand { get; init; }
    public BaseCommand SetWindowCommand  { get; init; }
    public BaseCommand CloseAppCommand { get; init; }
    public BaseCommand CloseInfoCommand { get; init; }
    public BaseCommand LogoutCommand { get; init; }

    #endregion

    #region commands methods

    void GoBack()
    {
        _navigationService.GoBack();
    }

    bool CanGoBack() => _navigationService.CanGoBack;

    void Logout()
    {
        //TODO ask for confirmation in dialog
        _appStateService.OnLoggingOut();
        _navigationService.NavigateTo(typeof(LoginViewModel), true);
        SetupInfo(SystemInfoStatus.Info, "Wylogowano", true);
    }

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

    void CloseInfo()
    {
        SetupInfo();
    }

    #endregion

    #region methods

    public void OnNavigationSelectionChanged(object obj)
    {
        if (obj is NavigationItem ni && !ni.Items.Any())
        {
            foreach (var child in Navigation)
                RestartSelection(child);
            SelectedItem = ni;
            _navigationService.NavigateTo(SelectedItem.ViewModelType, false);
        }
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
        //_navigationService.NavigateTo(typeof(DriveDetailViewModel), true);
        _navigationService.NavigateTo(typeof(LoginViewModel), true);
    }

    public void SetupInfo(SystemInfoStatus status = SystemInfoStatus.None, string text = "", bool isVisible = false)
    {
        InfoStatus = status;
        InfoText = text;
        IsInfoVisible = isVisible;
    }

    public void SetupLoading(bool isVisible = false, string text = "Wczytywanie...")
    {
        IsLoadingVisible = isVisible;
        LoadingText = text;
    }

    #endregion

    #region events

    private void _navigationService_OnNavigated(object? sender, MoriaBaseServices.Args.OnNavigatedEventArgs e)
    {
        if (_navigationService.IsOnGoBackNavigated)
        {
            foreach (var child in Navigation)
                RestartSelection(child);
        }

        if (e.Content is IViewModelContent content && content.GetViewModel() is ViewModelBase vmb)
        {
            PageTitle = vmb.Title;
            vmb.OnReAuthorizationNeeded -= OnReAuthorizationNeeded;
            vmb.OnReAuthorizationNeeded += OnReAuthorizationNeeded;

            NavigationItem mainNode = null;
            var navItem = GetCurrentNavigationItem(vmb.GetType(), ref mainNode);
            if (navItem != null)
            {
                mainNode.IsExpanded = true;
                SelectedItem = navItem;
            }
        }
        SetupInfo();
        SetupLoading();
        GoBackCommand?.RaiseCanExecuteChanged();
    }

    private async void OnReAuthorizationNeeded(object? sender, Args.InvokeViewEventArgs e)
    {
        //when logging in cancelled
        //e.CompletionSource.SetResult(null)
        //TODO show new window or popup with username/password fields to login again
        //or maybe password only with username control disabled
        var employee = await _tokenService.GetUserWithToken("123", "abc");

        e.CompletionSource.SetResult(employee != null && !string.IsNullOrWhiteSpace(employee.Token) && employee.ValidTo > DateTime.Now);
    }

    NavigationItem GetCurrentNavigationItem(Type viewModelType, ref NavigationItem mainNode)
    {
        mainNode = Navigation.FirstOrDefault(x => x.Items.Any(y => y.ViewModelType == viewModelType));
        if (mainNode != null)
            return mainNode.Items.First(x => x.ViewModelType == viewModelType);

        return null;
    }

    #endregion
}
