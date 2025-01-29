using MoriaModels.Models.Base;

namespace MoriaModels.Models.Orders;

public class Order : BaseModel
{
    //public int Id { get; set; }
    public string OrderNumberSymbol { get; set; }
    public string Remarks { get; set; }
    public string CatalogLink { get; set; }
    public string ClientSymbol { get; set; }

    public int OrderingContactId { get; set; }
    public Contact OrderingContact { get; set; }

    public int ReceivingContactId { get; set; }
    public Contact ReceivingContact { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
