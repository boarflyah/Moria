using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary;
using MoriaDesktop.ViewModels.Contacts;
using MoriaDesktop.Views.Base;
using MoriaDesktop.Views.Contacts;
using MoriaDesktop.Views.Dictionary;
using MoriaDesktopServices.Services;
using MoriaDesktop.Views.DriveComponents;
using MoriaDesktop.ViewModels.DriveComponents;
using MoriaDesktop.ViewModels.Products;
using MoriaDesktop.Views.Products;

namespace MoriaDesktop.Services;
public class DesktopPageService: PageService
{
    public DesktopPageService()
    {
        Register(typeof(LoginViewModel), typeof(LoginView));
        Register(typeof(SecondViewModel), typeof(SecondView));
        Register(typeof(WarehouseViewModel), typeof(WarehouseView));
        Register(typeof(MotorGearViewModel), typeof(MotorGearView));
        Register(typeof(MotorViewModel), typeof(MotorView));
        Register(typeof(ColorViewModel), typeof(ColorView));
        Register(typeof(SteelKindView), typeof(SteelKindView));
        Register(typeof(PositionViewModel), typeof(PositionView));
        Register(typeof(ContactViewModel), typeof(ContactView));
        Register(typeof(EmployeeDetailViewModel), typeof(EmployeeDetailView));
        Register(typeof(DriveDetailViewModel), typeof(DriveDetailView));
        Register(typeof(EmployeeListViewModel), typeof(EmployeeListView));
        Register(typeof(ProductDetailViewModel), typeof(ProductDetailView));
    }
}
