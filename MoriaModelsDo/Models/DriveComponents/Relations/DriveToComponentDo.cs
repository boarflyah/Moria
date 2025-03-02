using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.DriveComponents.Relations;
public class DriveToComponentDo: BaseDo
{

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
