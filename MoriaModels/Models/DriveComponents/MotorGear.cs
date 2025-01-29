using MoriaModels.Models.Base;

namespace MoriaModels.Models.DriveComponents;

public class MotorGear: BaseModel
{
    //public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string Ratio { get; set; }
}
