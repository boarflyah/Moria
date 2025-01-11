using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Contacts;
using MoriaDesktop.Views.Base;
using MoriaDesktop.Views.Contacts;
using MoriaDesktop.Views.Dictionary;
using MoriaDesktopServices.Services;
using MoriaDesktop.Views.Dictionary.DetailView;
using MoriaDesktop.Views.DriveComponents;
using MoriaDesktop.ViewModels.DriveComponents;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using MoriaDesktop.ViewModels.Products;
using MoriaDesktop.Views.Products;

namespace MoriaDesktop.Services;
public class DesktopPageService: PageService
{
    public DesktopPageService()
    {
        Register(typeof(LoginViewModel), typeof(LoginView));
        Register(typeof(SecondViewModel), typeof(SecondView));
        Register(typeof(WarehouseDetailViewModel), typeof(WarehouseDetailView));
        Register(typeof(MotorGearDetailViewModel), typeof(MotorGearDetailView));
        Register(typeof(MotorDetailViewModel), typeof(MotorDetailView));
        Register(typeof(ColorDetailViewModel), typeof(ColorDetailView));
        Register(typeof(SteelKindDetailView), typeof(SteelKindDetailView));
        Register(typeof(PositionDetailViewModel), typeof(PositionDetailView));
        Register(typeof(ContactDetailViewModel), typeof(ContactDetailView));
        Register(typeof(DriveDetailViewModel), typeof(DriveDetailView));

        Register(typeof(EmployeeListViewModel), typeof(EmployeeListView));
        Register(typeof(EmployeeDetailViewModel), typeof(EmployeeDetailView));
        Register(typeof(ProductDetailViewModel), typeof(ProductDetailView));
    }
}
