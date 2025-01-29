using MoriaModels.Models.Base;

namespace MoriaModels.Models.Orders;

public class Contact : BaseModel
{
    //public int Id { get; set; }
    public string ShortName { get; set; }
    public string LongName { get; set; }
    public string Symbol { get; set; }
}
