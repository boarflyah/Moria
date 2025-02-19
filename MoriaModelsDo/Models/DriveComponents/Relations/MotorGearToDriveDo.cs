using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.DriveComponents.Relations;
public class MotorGearToDriveDo: BaseDo
{
    private MotorGearDo _MotorGear;

    public MotorGearDo MotorGear
    {
        get => _MotorGear;
        set
        {
            _MotorGear = value;
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
