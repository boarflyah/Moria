using MoriaModels.Models.Base;

namespace MoriaModels.Models.DriveComponents;

public class Drive: BaseModel
{
    public int Id { get; set; }
    public bool Variator { get; set; }
    public bool Inverter { get; set; }
    public byte Quantity { get; set; }

    public int MotorId { get; set; }
    public Motor Motor { get; set; }

    public ICollection<MotorGear> Gearboxes { get; set; } = new List<MotorGear>();
}
