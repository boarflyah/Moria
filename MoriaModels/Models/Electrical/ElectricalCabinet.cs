using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Attributes;
using MoriaModels.Models.Base;

namespace MoriaModels.Models.Electrical;

[LookupHeaders(true, "Symbol")]
public class ElectricalCabinet : BaseModel
{
    [Searchable]
    public string Symbol { get; set; }
    public override LookupModel GetLookupObject() => new(Id, Symbol);
}
