using System.ComponentModel.DataAnnotations.Schema;
using MoriaModels.Attributes;
using MoriaModels.Models.Base;
using MoriaModels.Models.DriveComponents;
using MoriaModels.Models.DriveComponents.Relations;
using MoriaModels.Models.EntityPersonel;
using MoriaModels.Models.Orders.Relations;
using MoriaModels.Models.Products;
using MoriaModels.Models.Warehouses;

namespace MoriaModels.Models.Orders;

public class OrderItem : BaseModel
{
    //public int Id { get; set; }
    public int Index { get; set; }
    public string Description { get; set; }
    public string Notes { get; set; }
    public decimal MachineWeight { get; set; }
    public string TechnicalDrawingLink { get; set; }
    public double Quantity { get; set; }

    // only one choise 
    [Searchable]
    public Product? Product { get; set; }

    [Searchable]
    public Component? Component { get; set; }

    [Searchable]
    public Drive? Drive { get; set; }

    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }

    public int DesignerId { get; set; }

    [Searchable]
    public Employee Designer { get; set; }

    [NotMapped]
    public List<Component> Components
    {
        get;
    } = [];
    public List<ComponentToOrderItem> ComponentToOrderItems
    {
        get;
    } = [];
}
