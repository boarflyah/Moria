﻿using MoriaBaseModels.Models;
using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.Dictionaries;
public class ColorDo: BaseDo
{
    private string _Name;
    public string Name
    {
        get => _Name;       
        set
        {
            _Name = value;
            RaisePropertyChanged(value);
        }
    }

    private string _Code;
    public string Code
    {
        get => _Code;
        set
        {
            _Code = value;
            RaisePropertyChanged(value);
        }
    }

    public override void SetObject(LookupModel lookup)
    {
        base.SetObject(lookup);
        Name = lookup.Property1;
        Code = lookup.Property2;
    }
}
