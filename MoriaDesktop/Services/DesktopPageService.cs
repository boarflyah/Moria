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
using MoriaDesktop.Views.Dictionary.ListView;
using MoriaDesktop.ViewModels.Dictionary.ListView;
using MoriaDesktop.Views.Orders;
using MoriaDesktop.ViewModels.Orders;

namespace MoriaDesktop.Services;
public class DesktopPageService: PageService
{
    public DesktopPageService()
    {
        //DetailView
        Register(typeof(LoginViewModel), typeof(LoginView));
        Register(typeof(SecondViewModel), typeof(SecondView));
        Register(typeof(WarehouseDetailViewModel), typeof(WarehouseDetailView));
        Register(typeof(MotorGearDetailViewModel), typeof(MotorGearDetailView));
        Register(typeof(MotorDetailViewModel), typeof(MotorDetailView));
        Register(typeof(ColorDetailViewModel), typeof(ColorDetailView));
        Register(typeof(SteelKindDetailViewModel), typeof(SteelKindDetailView));
        Register(typeof(PositionDetailViewModel), typeof(PositionDetailView));
        Register(typeof(ContactDetailViewModel), typeof(ContactDetailView));
        Register(typeof(DriveDetailViewModel), typeof(DriveDetailView));
        Register(typeof(EmployeeDetailViewModel), typeof(EmployeeDetailView));
        Register(typeof(ProductDetailViewModel), typeof(ProductDetailView));
        Register(typeof(CategoryDetailViewModel), typeof(CategoryDetailView));
        Register(typeof(ComponentDetailViewModel), typeof(ComponentDetailView));
        Register(typeof(OrderDetailViewModel), typeof(OrderDetailView));
        Register(typeof(OrderItemDetailViewModel), typeof(OrderItemDetailView));

        //ListView
        Register(typeof(EmployeeListViewModel), typeof(EmployeeListView));
        Register(typeof(ColorListViewModel), typeof(ColorListView));
        Register(typeof(ContactListViewModel), typeof(ContactListView));
        Register(typeof(MotorGearListViewModel), typeof(MotorGearListView));
        Register(typeof(MotorListViewModel), typeof(MotorListView));
        Register(typeof(PositionListViewModel), typeof(PositionListView));
        Register(typeof(SteelKindListViewModel), typeof(SteelKindListView));
        Register(typeof(WarehouseListViewModel), typeof(WarehouseListView));
        Register(typeof(ProductListViewModel), typeof(ProductListView));
        Register(typeof(CategoryListViewModel), typeof(CategoryListView));
        Register(typeof(DriveListViewModel), typeof(DriveListView));
        Register(typeof(OrderListViewModel), typeof(OrderListView));
    }
}
