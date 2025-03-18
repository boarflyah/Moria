using System.ComponentModel.DataAnnotations.Schema;
using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Attributes;
using MoriaModels.Models.Base;
using MoriaModels.Models.DriveComponents;

namespace MoriaModels.Models.Products;

[LookupHeaders(true, "Symbol", true, "Nazwa")]
public class Product : BaseModel
{
    //public int Id { get; set; }

    [Searchable]
    public string Name { get; set; }

    [Searchable]
    public string Symbol { get; set; }
    public bool IsMainMachine { get; set; }

    [Searchable]
    public string SerialNumber { get; set; }

    public int? CategoryId { get; set; }

    [Searchable]
    public Category? Category { get; set; }

    public int? SteelKindId { get; set; }

    [Searchable]
    public SteelKind? SteelKind { get; set; }

    [InverseProperty(nameof(Component.Product))]
    public ICollection<Component> Components { get; set; } = new List<Component>();

    public override LookupModel GetLookupObject()
    {
        return new(Id, Symbol, Name);
    }
}
