using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Models.Base;

namespace MoriaModels.Models.Products;

[LookupHeaders(true, "Nazwa", true, "Symbol")]
public class SteelKind : BaseModel
{
    //public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }

    public override LookupModel GetLookupObject() => new(Id, Name, Symbol);
}
