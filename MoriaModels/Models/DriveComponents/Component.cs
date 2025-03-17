using System.ComponentModel.DataAnnotations.Schema;
using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Attributes;
using MoriaModels.Models.Base;
using MoriaModels.Models.DriveComponents.Relations;
using MoriaModels.Models.Orders;
using MoriaModels.Models.Orders.Relations;
using MoriaModels.Models.Products;

namespace MoriaModels.Models.DriveComponents;

[LookupHeaders(true, "Produkt", true, "Ilość", true, "Kolor")]
public class Component : BaseModel
{
    [ForeignKey(nameof(Product))]
    public int ProductId
    {
        get; set;
    }

    [Searchable]
    public Product Product
    {
        get; set;
    }

    [Searchable]
    public Product? ComponentProduct
    {
        get; set;
    }

    [Searchable]
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

    [Searchable]
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
        => new(Id, ComponentProduct.Name, Quantity.ToString(), ComponentColor?.Name);
}
