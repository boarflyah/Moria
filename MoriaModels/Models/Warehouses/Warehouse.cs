using MoriaModels.Models.Products;

namespace MoriaModels.Models.Warehouses;

public class Warehouse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
