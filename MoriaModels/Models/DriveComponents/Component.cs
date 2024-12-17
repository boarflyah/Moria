using MoriaModels.Models.Base;
using MoriaModels.Models.Orders;
using MoriaModels.Models.Products;

namespace MoriaModels.Models.DriveComponents;

public class Component: BaseModel
{
    public int Id { get; set; }
    public string ElectricalDescription { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
