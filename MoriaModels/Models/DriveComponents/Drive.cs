using MoriaModels.Models.Base;
using MoriaModels.Models.DriveComponents.Relations;

namespace MoriaModels.Models.DriveComponents;

public class Drive: BaseModel
{
    //public int Id { get; set; }
    public bool Variator { get; set; }
    public bool Inverter { get; set; }
    public byte Quantity { get; set; }
    public Motor? Motor { get; set; }
    public List<MotorGear> MotorGears
    {
        get;
    } = [];
    public List<MotorGearToDrive> MotorGearToDrives
    {
        get;
    } = [];

    public List<Component> Components
    {
        get;
    } = [];
    public List<DriveToComponent> DriveToComponents
    {
        get;
    } = [];
}
