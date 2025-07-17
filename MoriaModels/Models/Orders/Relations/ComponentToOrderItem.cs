using MoriaModels.Models.Base;
using MoriaModels.Models.DriveComponents;
using MoriaModels.Models.Products;

namespace MoriaModels.Models.Orders.Relations;

public class ComponentToOrderItem : BaseModel
{
    public Color Color { get; set; }
    public string ElectricalDescription { get; set; }

    public int ComponentId { get; set; }
    public Component Component { get; set; } = null!;

    public int OrderItemId { get; set; }
    public OrderItem OrderItem { get; set; } = null!;

    public int DriveId
    {
        get; set;
    }
    public Drive Drive { get; set; } = null!;

    public int Quantity
    {
        get; set;
    }

}
