using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Models.Base;

namespace MoriaModels.Models.Orders;

[LookupHeaders(true, "Symbol", true, "Nazwa skrócona", true, "Nazwa długa")]
public class Contact : BaseModel
{
    //public int Id { get; set; }
    public string ShortName { get; set; }
    public string LongName { get; set; }
    public string Symbol { get; set; }

    public override LookupModel GetLookupObject()
        => new(Id, Symbol, ShortName, LongName);
}
