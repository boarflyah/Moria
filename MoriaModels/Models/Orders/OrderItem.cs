using System.ComponentModel.DataAnnotations.Schema;
using MoriaModels.Attributes;
using MoriaModels.Models.Base;
using MoriaModels.Models.DriveComponents;
using MoriaModels.Models.DriveComponents.Relations;
using MoriaModels.Models.EntityPersonel;
using MoriaModels.Models.Interfaces;
using MoriaModels.Models.Orders.Relations;
using MoriaModels.Models.Products;
using MoriaModels.Models.Warehouses;

namespace MoriaModels.Models.Orders;

public class OrderItem : BaseModel, ISubiektModel
{
    //public int Id { get; set; }
    public int Index { get; set; }
    public string Symbol { get; set; }
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

    public int? WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }


    public int? MainColorId { get; set; }
    public Color? MainColor { get; set; }

    public int? DetailsColorId { get; set; }
    public Color? DetailsColor { get; set; }

    public int? DesignerId { get; set; }

    [Searchable]
    public Employee? Designer { get; set; }

    public int SubiektId
    {
        get; set;
    }
    public decimal Power { get; set; }
    public string ElectricalDescription { get; set; }
    #region
    public bool TechnicalDrawingCompleted { get; set; }
    public bool CuttingCompleted { get; set; }
    public bool MetalCliningCompleted { get; set; }
    public bool PaintingCompleted { get; set; }
    public bool ElectricaCabinetCompleted { get; set; }
    public bool MachineAssembled { get; set; }
    public bool MachineWiredAndTested { get; set; }
    public bool MachineReleased { get; set; }
    public bool TransportOrdered { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime DueDate { get; set; }
    #endregion

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
