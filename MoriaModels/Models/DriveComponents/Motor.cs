using MoriaModels.Models.Base;

namespace MoriaModels.Models.DriveComponents;

public class Motor: BaseModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public decimal Power { get; set; }
}
