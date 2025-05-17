using MoriaModels.Models.Base;
using MoriaModels.Models.DriveComponents;
using MoriaModels.Models.Electrical;
using MoriaModels.Models.EntityPersonel;
using MoriaModels.Models.Orders;
using MoriaModels.Models.Products;
using MoriaModels.Models.Warehouses;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;
using MoriaModelsDo.Models.Orders;
using MoriaModelsDo.Models.Products;

namespace MoriaWebAPIServices.Services;
/// <summary>
/// Need to register all BaseDo/BaseModel pairs used in desktop app
/// </summary>
public class ModelTypeConverter
{
    readonly Dictionary<Type, Type> RegisteredTypes = new();

    public ModelTypeConverter()
    {
        Register<ContactDo, Contact>();
        Register<EmployeeDo, Employee>();
        Register<PositionDo, Position>();
        Register<ColorDo, Color>();
        Register<SteelKindDo, SteelKind>();
        Register<WarehouseDo, Warehouse>();
        Register<DriveDo, Drive>();
        Register<MotorDo, Motor>();
        Register<MotorGearDo, MotorGear>();
        Register<CategoryDo, Category>();
        Register<ProductDo, Product>();
        Register<OrderDo, Order>();
        Register<OrderItemDo, OrderItem>();
        Register<ComponentDo, Component>();
        Register<ElectricalCabinetDo, ElectricalCabinet>();
    }

    protected void Register<T1, T2>() 
        where T1 : BaseDo 
        where T2 : BaseModel
    {
        RegisteredTypes.Add(typeof(T1), typeof(T2));
    }

    public Type GetModelType(Type doType)
    {
        return RegisteredTypes.GetValueOrDefault(doType);
    }
}
