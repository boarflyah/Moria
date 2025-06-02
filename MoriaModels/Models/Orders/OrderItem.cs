using System.ComponentModel.DataAnnotations.Schema;
using MoriaModels.Attributes;
using MoriaModels.Models.Base;
using MoriaModels.Models.DriveComponents;
using MoriaModels.Models.DriveComponents.Relations;
using MoriaModels.Models.Electrical;
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
    public int MachineWeight { get; set; }
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
    public string SerialNumber
    {
        get; set;
    }
    public string ProductionYear
    {
        get; set;
    }
    public string ElectricalDescription { get; set; }
    #region
    [Column(TypeName = "timestamp without time zone")]
    public DateTime? TechnicalDrawingCompleted { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? CuttingCompleted { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? WeldingCompleted { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? MetalCliningCompleted { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? PaintingCompleted { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ElectricaCabinetCompleted { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? MachineAssembled { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? MachineWiredAndTested { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? MachineReleased { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? TransportOrdered { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime DueDate { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? PlannedTransport { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? PlannedMachineWiredAndTested { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? PlannedMachineAssembled { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ElectricalDiagramCompleted { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ControlCabinetWorkStartDate { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ControlCabinetWorkEndDate { get; set; }
    public ElectricalCabinet ElectricalCabinet { get; set; }
    [Searchable]
    public Employee? Electrician { get; set; }
    #endregion

    public int? OrderId
    {
        get; set;
    }

    [Searchable]
    public Order? Order
    {
        get; set;
    }

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
