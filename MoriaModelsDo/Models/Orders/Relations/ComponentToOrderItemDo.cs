using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaModelsDo.Models.Orders.Relations;
public class ComponentToOrderItemDo: BaseDo
{
    private ComponentDo _Component;
    public ComponentDo Component
    {
        get => _Component;
        set
        {
            _Component = value;
            RaisePropertyChanged(value);
        }
    }

    private OrderItemDo _OrderItem;
    public OrderItemDo OrderItem
    {
        get => _OrderItem;
        set
        {
            _OrderItem = value;
            RaisePropertyChanged(value);
        }
    }

    private ColorDo _Color;
    public ColorDo Color
    {
        get => _Color;
        set
        {
            _Color = value;
            RaisePropertyChanged(value);
        }
    }

    private DriveDo _Drive;
    public DriveDo Drive
    {
        get => _Drive;
        set
        {
            _Drive = value;
            RaisePropertyChanged(value);
        }
    }

    private int _Quantity;
    public int Quantity
    {
        get => _Quantity;
        set
        {
            _Quantity = value;
            RaisePropertyChanged(value);
        }
    }
}
