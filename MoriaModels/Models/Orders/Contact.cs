using MoriaModels.Attributes;
using MoriaModels.Models.Base;

namespace MoriaModels.Models.Orders;

public class Contact : BaseModel
{
    //public int Id { get; set; }
    [Searchable]
    public string ShortName { get; set; }
    [Searchable]
    public string LongName { get; set; }
    [Searchable]
    public string Symbol { get; set; }
}
