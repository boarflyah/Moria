using System.ComponentModel.DataAnnotations.Schema;
using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Models.Base;
using MoriaModels.Models.Orders;
using MoriaModels.Models.Products;

namespace MoriaModels.Models.DriveComponents;

[LookupHeaders(true, "Produkt", true, "Ilość", true, "Specyfikacja elektryczna")]
public class Component : BaseModel
{
    public string ElectricalDescription
    {
        get; set;
    }

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

    public double Quantity
    {
        get; set;
    }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public override LookupModel GetLookupObject()
        => new(Id, Product.Name, Quantity.ToString(), ElectricalDescription);
}
