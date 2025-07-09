using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Attributes;
using MoriaModels.Models.Base;

namespace MoriaModels.Models.DriveComponents;

[LookupHeaders(true, "Typ", true, "Moc")]
public class Inverter : BaseModel
{
    [Searchable]
    public string Type { get; set; }

    [Searchable]
    public decimal Power { get; set; }
    public override LookupModel GetLookupObject()
=> new(Id, Type, Power.ToString("n2"));
}
