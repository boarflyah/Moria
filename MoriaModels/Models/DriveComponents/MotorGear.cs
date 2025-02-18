using MoriaModels.Models.Base;
using MoriaModels.Models.DriveComponents.Relations;

namespace MoriaModels.Models.DriveComponents;

public class MotorGear: BaseModel
{
    //public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string Ratio { get; set; }
    public List<Drive> Drives
    {
        get;
    } = [];
    public List<MotorGearToDrive> MotorGeardToDrives
    {
        get;
    } = [];
}
