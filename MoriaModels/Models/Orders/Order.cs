using MoriaModels.Attributes;
using MoriaModels.Models.Base;

namespace MoriaModels.Models.Orders;

public class Order : BaseModel
{
    //public int Id { get; set; }
    [Searchable]
    public string OrderNumberSymbol { get; set; }
    public string Remarks { get; set; }
    public string CatalogLink { get; set; }
    public string ClientSymbol { get; set; }

    public int OrderingContactId { get; set; }

    [Searchable]
    public Contact OrderingContact { get; set; }

    public int ReceivingContactId { get; set; }

    [Searchable]
    public Contact ReceivingContact { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
