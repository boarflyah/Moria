using MoriaModels.Attributes;
using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Models.Base;
using MoriaModels.Models.Interfaces;

namespace MoriaModels.Models.Orders;

[LookupHeaders(true, "Symbol", true, "Nazwa skrócona", true, "Nazwa długa")]
public class Contact : BaseModel, ISubiektModel
{
    //public int Id { get; set; }
    [Searchable]
    public string ShortName { get; set; }
    [Searchable]
    public string LongName { get; set; }
    [Searchable]
    public string Symbol { get; set; }
    public int SubiektId
    {
        get; set;
    }

    public override LookupModel GetLookupObject()
        => new(Id, Symbol, ShortName, LongName);
}
