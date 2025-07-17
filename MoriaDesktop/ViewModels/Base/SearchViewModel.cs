using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MoriaBaseServices;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Orders;
using MoriaDesktopServices.Interfaces.API;
using MoriaDesktopServices.Interfaces;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Orders;

namespace MoriaDesktop.ViewModels.Base;

public class SearchViewModel : BaseDetailViewModel
{
    readonly IApiOrderService _orderService;

    public SearchViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, INavigationService navigationService, IKeepAliveWorker keepAliveWorker, IApiOrderService orderService) : base(logger, appStateService, apiLockService, navigationService, keepAliveWorker)
    {
        _orderService = orderService;
    }

    public ObservableCollection<OrderDo> Orders
    {
        get; set;
    } = new();

    private string _Search;
    public string Search
    {
        get => _Search;
        set
        {
            _Search = value;
            RaisePropertyChanged(value);
        }
    }

    public override void Clear()
    {
        throw new NotImplementedException();
    }

    public override BaseDo GetDo()
    {
        throw new NotImplementedException();
    }

    public override Type GetModelType() => null;

    protected override Task LoadObject()
    {
        throw new NotImplementedException();
    }

    protected override Task<bool> SaveNewObject()
    {
        throw new NotImplementedException();
    }

    protected override Task<bool> UpdateExistingObject()
    {
        throw new NotImplementedException();
    }

    public async override Task Load()
    {
       
        try
        {
            var product = await ExecuteApiRequest(_orderService.GetSearchOrder, _appStateService.LoggedUser.Username, Search);

            if (product != null)
            {
               Orders.Add(product);
            }
            else
                _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
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

    public void RedirectToOrderDetails(OrderDo order)
    {
        _navigationService.NavigateTo(typeof(OrderDetailViewModel), true, order.Id);
    }

    public override void OnNavigatedTo(params object[] parameters)
    {
        base.OnNavigatedTo(parameters);
        Search = (string)parameters.FirstOrDefault();
    }
}
