using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaBaseModels.Models;
using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.DriveComponents;

public class BrakeDo : BaseDo
{
    private string _Kind;
    public string Kind
    {
        get => _Kind;
        set
        {
            _Kind = value;
            RaisePropertyChanged(value);
        }
    }

    public override void SetObject(LookupModel lookup)
    {
        base.SetObject(lookup);
        Kind = lookup.Property1;
    }
}
