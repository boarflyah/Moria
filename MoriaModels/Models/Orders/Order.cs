using System.ComponentModel.DataAnnotations.Schema;
using MoriaModels.Attributes;
using MoriaModels.Models.Base;
using MoriaModels.Models.Interfaces;

namespace MoriaModels.Models.Orders;

public class Order : BaseModel, ISubiektModel
{
    //public int Id { get; set; }
    [Searchable]
    public string OrderNumberSymbol { get; set; }
    public string Remarks { get; set; }
    public string CatalogLink { get; set; }
    public string SalesOfferLink { get; set; }
    public string ClientSymbol { get; set; }

    public int? OrderingContactId { get; set; }

    [Searchable]   
    public Contact? OrderingContact { get; set; }

    public int? ReceivingContactId { get; set; }

    [Searchable]
    public Contact? ReceivingContact { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime OrderDate
    {
        get; set;
    }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime DueDate
    {
        get; set;
    }

    public int SubiektId
    {
        get;
        set;
    }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
