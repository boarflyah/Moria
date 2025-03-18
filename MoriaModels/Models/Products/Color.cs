using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Attributes;
using MoriaModels.Models.Base;

namespace MoriaModels.Models.Products;

[LookupHeaders(true, "Nazwa", true, "Kod")]
public class Color: BaseModel
{
    //public int Id { get; set; }

    [Searchable]
    public string Name { get; set; }
    public string Code { get; set; }

    public override LookupModel GetLookupObject()
        => new(Id, Name, Code);
}
