using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Attributes;
using MoriaModels.Models.Base;

namespace MoriaModels.Models.Products;

[LookupHeaders(true, "Nazwa", true, "Symbol")]
public class SteelKind : BaseModel
{
    //public int Id { get; set; }

    [Searchable]
    public string Name { get; set; }

    [Searchable]
    public string Symbol { get; set; }

    public override LookupModel GetLookupObject() => new(Id, Name, Symbol);
}
