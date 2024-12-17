using MoriaModels.Models.Base;
using MoriaModels.Models.EntityPersonel;
using MoriaModels.Models.Products;
using MoriaModels.Models.Warehouses;

namespace MoriaModels.Models.Orders;

public class OrderItem : BaseModel
{
    public int Id { get; set; }
    public int Index { get; set; }
    public string Description { get; set; }
    public string Notes { get; set; }
    public decimal MachineWeight { get; set; }
    public string TechnicalDrawingLink { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }

    public int DesignerId { get; set; }
    public Employee Designer { get; set; }
}
