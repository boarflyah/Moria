using System.ComponentModel.DataAnnotations.Schema;
using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Models.Base;
using MoriaModels.Models.DriveComponents;

namespace MoriaModels.Models.Products;

[LookupHeaders(true, "Symbol", true, "Nazwa")]
public class Product : BaseModel
{
    //public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public bool IsMainMachine { get; set; }
    public string SerialNumber { get; set; }

    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    public int? SteelKindId { get; set; }
    public SteelKind? SteelKind { get; set; }

    [InverseProperty(nameof(Component.Product))]
    public ICollection<Component> Components { get; set; } = new List<Component>();

    public override LookupModel GetLookupObject()
    {
        return new(Id, Symbol, Name);
    }
}
