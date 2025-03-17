using MoriaModels.Attributes;
using MoriaModels.Models.Base;
using MoriaModels.Models.Products;

namespace MoriaModels.Models.Warehouses;

public class Warehouse : BaseModel
{
    //public int Id { get; set; }

    [Searchable]
    public string Name { get; set; }

    [Searchable]
    public string Symbol { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
