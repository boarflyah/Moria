﻿using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Attributes;
using MoriaModels.Models.Base;

namespace MoriaModels.Models.DriveComponents;

[LookupHeaders(true, "Symbol", true, "Nazwa", true, "Moc (kV)")]
public class Motor: BaseModel
{
    //public int Id { get; set; }
    [Searchable]
    public string Name { get; set; }

    [Searchable]
    public string Symbol { get; set; }
    public decimal Power { get; set; }

    public override LookupModel GetLookupObject()
        => new(Id, Symbol, Name, Power.ToString("n2"));
}
