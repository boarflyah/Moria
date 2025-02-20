using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Models.Base;

namespace MoriaModels.Models.DriveComponents;

[LookupHeaders(true, "Symbol", true, "Nazwa", true, "Moc (kV)")]
public class Motor: BaseModel
{
    //public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public decimal Power { get; set; }

    public override LookupModel GetLookupObject()
        => new(Id, Symbol, Name, Power.ToString("n2"));
}
