using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Attributes;
using MoriaModels.Models.Base;
using MoriaModels.Models.DriveComponents.Relations;

namespace MoriaModels.Models.DriveComponents;

[LookupHeaders(true, "Symbol", true, "Nazwa", true, "Przełożenie")]
public class MotorGear: BaseModel
{
    //public int Id { get; set; }

    [Searchable]
    public string Name { get; set; }

    [Searchable]
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

    public override LookupModel GetLookupObject()
        => new(Id, Symbol, Name, Ratio);
}
