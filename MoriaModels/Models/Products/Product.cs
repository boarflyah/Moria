using MoriaModels.Models.DriveComponents;

namespace MoriaModels.Models.Products;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public bool IsMainMachine { get; set; }
    public string SerialNumber { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public int SteelKindId { get; set; }
    public SteelKind SteelKind { get; set; }

    public ICollection<Component> Components { get; set; } = new List<Component>();
}
