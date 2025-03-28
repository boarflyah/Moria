using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.Extensions.Logging;
using MoriaBaseServices;
using MoriaDesktop.Services;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Orders;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MoriaDesktop.ViewModels.Base;

public class CalendarViewModel : BaseDetailViewModel
{
    readonly IApiOrderService _orderService;

    public CalendarViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService, IApiLockService apiLockService, INavigationService navigationService, IKeepAliveWorker keepAliveWorker, IApiOrderService orderService) : base(logger, appStateService, apiLockService, navigationService, keepAliveWorker)
    {
        _orderService = orderService;
    }

    public ObservableCollection<OrderDo> Orders
    {
        get; set;
    } = new();

    private DateTime _SelectedDate;
    public DateTime SelectedDate
    {
        get => _SelectedDate;
        set
        {
            _SelectedDate = value;
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

    public override Type GetModelType()
    {
        throw new NotImplementedException();
    }

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
        CultureInfo culture = CultureInfo.CurrentCulture;
        int weekNumber = culture.Calendar.GetWeekOfYear(SelectedDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        try
        {
            var products = await ExecuteApiRequest(_orderService.GetCalendarOrders, _appStateService.LoggedUser.Username, weekNumber);

            if (products != null && products.Any())
            {
                foreach (var product in products)
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


    public override void OnNavigatedTo(params object[] parameters)
    {
        base.OnNavigatedTo(parameters);
        SelectedDate = (DateTime)parameters.FirstOrDefault();
    }
}
