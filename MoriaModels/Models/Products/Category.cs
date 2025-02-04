using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Models.Base;

namespace MoriaModels.Models.Products;

[LookupHeaders(true, "Nazwa")]
public class Category : BaseModel
{
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();

    public override LookupModel GetLookupObject() => new(Id, Name);
}
