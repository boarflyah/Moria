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
using MoriaDesktop.ViewModels.Orders;
using MoriaDesktop.Args;
using CommunityToolkit.Mvvm.Input;
using MoriaBaseServices;

namespace MoriaDesktop.ViewModels.Base;

public class MainWindowViewModel : BaseNotifyPropertyChanged
{
    #region DI properties

    readonly INavigationService _navigationService;
    readonly IApiTokenService _tokenService;
    readonly AppStateService _appStateService;
    readonly IApiOrderService _orderService;
    readonly ILogger<MainWindowViewModel> _logger;

    #endregion

    public MainWindowViewModel(ILogger<MainWindowViewModel> logger, INavigationService navigationService, IApiTokenService tokenService, AppStateService appStateService,
        IApiOrderService orderService) : base()
    {
        _navigationService = navigationService;
        _tokenService = tokenService;
        _appStateService = appStateService;
        _logger = logger;
        _orderService = orderService;


        _navigationService.OnNavigated += OnNavigated;

        GoBackCommand = new(GoBack, CanGoBack);
        SetFullScreenCommand = new(SetFullScreen, CanSetFullScreen);
        SetWindowCommand = new(SetWindow, CanSetWindow);
        CloseAppCommand = new(CloseApp, CanCloseApp);
        CloseInfoCommand = new(CloseInfo);
        LogoutCommand = new(Logout);
        ImportOrdersCommand = new(ImportOrders, CanImportOrders);

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
        NavigationItem node5 = new()
        {
            Title = "Zamówienia",
            Items = new()
           
        };
        //NavigationItem node3 = new()
        //{
        //    Title = "Testy",
        //    Items = new()
        //};

        Navigation.Add(node1);
        Navigation.Add(node2);
        Navigation.Add(node4);
        Navigation.Add(node5);
        //Navigation.Add(node3);

        node5.Items.Add(new()
        {
            Title = "Zamówienia",
            ViewModelType = typeof(OrderListViewModel)
        });
        node5.Items.Add(new()
        {
            Title = "Elektrycy zam.",
            ViewModelType = typeof(ElectricalOrderItemListViewModel)
        });

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
        node2.Items.Add(new()
        {
            Title = "Szafy elektryczne",
            ViewModelType = typeof(ElectricalCabinetListViewModel),
        });
        node2.Items.Add(new()
        {
            Title = "Hamulce",
            ViewModelType = typeof(BrakeListViewModel),
        });
        node2.Items.Add(new()
        {
            Title = "Chłodzenie zewnętrzne",
            ViewModelType = typeof(ExternalCoolingListViewModel),
        });
        node2.Items.Add(new()
        {
            Title = "Falowniki",
            ViewModelType = typeof(InverterListViewModel),
        });
        node2.Items.Add(new()
        {
            Title = "Pompy",
            ViewModelType = typeof(PumpListViewModel),
        });
        node2.Items.Add(new()
        {
            Title = "Wyposażenie dodatkowe",
            ViewModelType = typeof(SupplementListViewModel),
        });
        node2.Items.Add(new()
        {
            Title = "Wariator",
            ViewModelType = typeof(VariatorListViewModel),
        });


        //node3.Items.Add(new()
        //{
        //    Title = "Test",
        //    ViewModelType = typeof(ColorDetailViewModel),
        //});

        node4.Items.Add(new()
        {
            Title = "Produkty",
            ViewModelType = typeof(ProductListViewModel)
        });
        node4.Items.Add(new()
        {
            Title = "Napędy",
            ViewModelType = typeof(DriveListViewModel)
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

    private bool _IsAdminViewing;
    public bool IsAdminViewing
    {
        get => _IsAdminViewing;
        set
        {
            _IsAdminViewing = value;
            RaisePropertyChanged(value);
        }
    }


    private bool _IsImportInProgress;
    public bool IsImportInProgress
    {
        get => _IsImportInProgress;
        set
        {
            _IsImportInProgress = value;
            RaisePropertyChanged(value);
        }
    }

    public event EventHandler<ConfirmationEventArgs> OnConfirmationRequired;

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
    public AsyncRelayCommand ImportOrdersCommand
    {
        get; init;
    }

    #endregion

    #region commands methods

    void GoBack()
    {
        _navigationService.GoBack();
    }

    bool CanGoBack() => _navigationService.CanGoBack;

    async void Logout()
    {
        var confirmation = await _appStateService.ConfirmAsync("Czy na pewno chcesz się wylogować?");
        if (confirmation == true)
        {
            _appStateService.OnLoggingOut();
            _navigationService.NavigateTo(typeof(LoginViewModel), true);
            SetupInfo(SystemInfoStatus.Info, "Wylogowano", true);
        }
    }

    public async Task<bool> ConfirmClosing()
    {
        var confirmation = await _appStateService.ConfirmAsync("Czy na pewno chcesz zamknąć program?");
        if (confirmation == true)
            return true;
        return false;
    }

    async Task ImportOrders()
    {
        if (_appStateService?.LoggedUser != null)
        {
            try
            {
                IsImportInProgress = true;
                ImportOrdersCommand?.NotifyCanExecuteChanged();
                var confirmation = await _appStateService.ConfirmAsync("Czy chcesz zaimportować zamówienia z Subiekta?");
                if (confirmation == true)
                {
                    await _orderService.ImportOrders(_appStateService.LoggedUser.Username);
                    _appStateService.SetupInfo(SystemInfoStatus.Info, "Import zamówień z Subiekta został zakończony", true);
                }
            }
            catch (MoriaApiException mae)
            {
                _appStateService.SetupInfo(SystemInfoStatus.Warning, mae.AdditionalMessage, true);
            }
            catch (Exception ex)
            {
                //_appStateService.SetupInfo(SystemInfoStatus.Error, "Nie udało się zaimportować zamówień, więcej informacji w pliku zdarzeń na serwerze", true);
                _appStateService.SetupInfo(SystemInfoStatus.Error, ex.Message, true);
            }
            finally
            {
                IsImportInProgress = false;
                ImportOrdersCommand?.NotifyCanExecuteChanged();
            }
        }
    }

    bool CanImportOrders() => _appStateService?.LoggedUser?.Admin == true && !IsImportInProgress;

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

    public void NavigateToCalendar(DateTime date)
    {
        _navigationService.NavigateTo(typeof(CalendarViewModel), false, date);
    }

    public void NavigateToSearch(string text)
    {
        _navigationService.NavigateTo(typeof(SearchViewModel), false, text);
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

    public async Task<bool?> ConfirmationRequired(object sender, ConfirmationEventArgs args)
    {
        args.CompletionSource = new();
        OnConfirmationRequired?.Invoke(sender, args);
        return await args.CompletionSource.Task;
    }

    public void SetupTitle(string title)
    {
        _navigationService.SetTabTitle(title);
    }

    #endregion

    #region events

    public void OnNavigated(object? sender, MoriaBaseServices.Args.OnNavigatedEventArgs e)
    {
        if (_navigationService.IsOnGoBackNavigated)
        {
            foreach (var child in Navigation)
                RestartSelection(child);
        }

        if (e.Content is IViewModelContent content && content.GetViewModel() is ViewModelBase vmb)
        {
            PageTitle = vmb.Title;
            _appStateService.SetupTitle(vmb.Title);
            vmb.OnReAuthorizationNeeded -= OnReAuthorizationNeeded;
            vmb.OnReAuthorizationNeeded += OnReAuthorizationNeeded;

            NavigationItem mainNode = null;
            var navItem = GetCurrentNavigationItem(vmb.GetType(), ref mainNode);
            if (navItem != null)
            {
                mainNode.IsExpanded = true;
                SelectedItem = navItem;
            }
            else
            {
                foreach (var child in Navigation)
                    RestartSelection(child);
            }
        }
        else
            _appStateService.SetupTitle("Pusta strona");
        SetupInfo();
        SetupLoading();
        GoBackCommand?.RaiseCanExecuteChanged();
        IsAdminViewing = _appStateService.LoggedUser?.Admin == true;
        ImportOrdersCommand?.NotifyCanExecuteChanged();
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
