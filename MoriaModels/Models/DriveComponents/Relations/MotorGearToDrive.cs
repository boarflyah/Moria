using MoriaModels.Models.Base;

namespace MoriaModels.Models.DriveComponents.Relations;
public class MotorGearToDrive: BaseModel
{
    public int DriveId
    {
        get; set;
    }
    public int MotorGearId
    {
        get; set;
    }
    public Drive Drive
    {
        get; set;
    } = null!;
    public MotorGear MotorGear
    {
        get; set;
    } = null!;
    public int Quantity
    {
        get; set;
    }
}
