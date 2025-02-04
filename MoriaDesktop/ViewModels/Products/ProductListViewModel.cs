using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Contacts;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.API;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.ViewModels.Products;
internal class ProductListViewModel : BaseListViewModel
{
    readonly IApiEmployeeService _employeeService;

    public ProductListViewModel(ILogger<BaseListViewModel> logger, AppStateService appStateService, INavigationService navigationService) : base(logger, appStateService, navigationService)
    {
    }

    #region properties

    public ObservableCollection<ProductDo> Products
    {
        get; set;
    }

    #endregion

    #region commands

    protected async override Task LoadList()
    {
        Products.Clear();

        var products = await ExecuteApiRequest(_employeeService.GetEmployees, _appStateService.LoggedUser.Username);
        if (products != null)
        {
            //foreach (var product in products)
            //    Products.Add(product);
        }
        else
            _appStateService.SetupInfo(Models.Enums.SystemInfoStatus.Info, "Brak danych do wczytania", true);
    }

    public override void OnRowSelected(object row)
    {
        if (row is EmployeeDo edo)
            _navigationService.NavigateTo(typeof(EmployeeDetailViewModel), false, edo.Id);
    }

    protected override void New() =>
        _navigationService.NavigateTo(typeof(EmployeeDetailViewModel), false, null);

    protected async override Task<bool> SendDeleteRequest() =>
        await ExecuteApiRequest(_employeeService.DeleteEmployee, _appStateService.LoggedUser.Username, (Selected as EmployeeDo)?.Id ?? 0);

    #endregion
}
