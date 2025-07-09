using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Attributes;
using MoriaModels.Models.Base;
using MoriaModels.Models.Products;

namespace MoriaModels.Models.DriveComponents;

[LookupHeaders(true, "Rodzaj")]
public class Brake : BaseModel
{

    [Searchable]
    public string Kind { get; set; }

    public override LookupModel GetLookupObject() => new(Id, Kind);
}
