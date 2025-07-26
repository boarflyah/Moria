using System.ComponentModel.DataAnnotations.Schema;
using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Attributes;
using MoriaModels.Models.Base;
using MoriaModels.Models.DriveComponents.Relations;

namespace MoriaModels.Models.DriveComponents;

//[LookupHeaders(true, "Nazwa", true, "Ilość", true, "Falownik", true, "Wariator")]
[LookupHeaders(true, "Nazwa", true, "Ilość")]
public class Drive: BaseModel
{
    //public int Id { get; set; }
    [Searchable]
    public Variator? Variator { get; set; }
    [Searchable]
    public Inverter? Inverter { get; set; }
    [Searchable]
    public Pump? Pump
    {
        get;
        set;
    }
    [Searchable]
    public ExternalCooling? ExternalCooling
    {
        get; set;
    }
    [Searchable]
    public Brake? Brake
    {
        get; set;
    }
    [Searchable]
    public Supplement? Supplement
    {
        get; set;
    }
    public byte Quantity { get; set; }

    [Searchable]
    public Motor? Motor { get; set; }

    [Searchable]
    public string Name
    {
        get; set;
    }

    [NotMapped]
    public List<MotorGear> MotorGears
    {
        get;
    } = [];
    public List<MotorGearToDrive> MotorGearToDrives
    {
        get;
    } = [];

    [NotMapped]
    public List<Component> Components
    {
        get;
    } = [];
    public List<DriveToComponent> DriveToComponents
    {
        get;
    } = [];

    public override LookupModel GetLookupObject()
        //=> new(Id, Name, Quantity.ToString(), Inverter?.Type ?? string.Empty, Variator?.Type ?? string.Empty );
        => new(Id, Name, Quantity.ToString());
}
