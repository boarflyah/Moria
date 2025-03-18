using System.ComponentModel.DataAnnotations.Schema;
using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Models.Base;
using MoriaModels.Models.DriveComponents.Relations;
using MoriaModels.Models.Orders;
using MoriaModels.Models.Orders.Relations;
using MoriaModels.Models.Products;

namespace MoriaModels.Models.DriveComponents;

[LookupHeaders(true, "Nazwa", true, "Ilość")]
public class Component : BaseModel
{
    [ForeignKey(nameof(Product))]
    public int ProductId
    {
        get; set;
    }

    public Product Product
    {
        get; set;
    }

    public Product? ComponentProduct
    {
        get; set;
    }

    public string Name
    {
        get; set;
    }

    public double Quantity
    {
        get; set;
    }

    public Color? ComponentColor
    {
        get; set;
    }

    public List<Drive> Drives
    {
        get;
    } = [];
    public List<DriveToComponent> DriveToComponents
    {
        get;
    } = [];

    [NotMapped]
    public List<OrderItem> OrderItems
    {
        get;
    } = [];

    public List<ComponentToOrderItem> ComponentToOrders
    {
        get;
    } = [];

    public override LookupModel GetLookupObject()
        => new(Id, Name, Quantity.ToString());
}
