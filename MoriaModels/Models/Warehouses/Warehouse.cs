using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Attributes;
using MoriaModels.Models.Base;
using MoriaModels.Models.Products;

namespace MoriaModels.Models.Warehouses;

[LookupHeaders(true, "Symbol", true, "Nazwa")]
public class Warehouse : BaseModel
{
    //public int Id { get; set; }

    [Searchable]
    public string Name { get; set; }

    [Searchable]
    public string Symbol { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();

    public override LookupModel GetLookupObject() => new(Id, Symbol, Name);
}
