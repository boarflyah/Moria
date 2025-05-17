using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoriaBaseModels.Models;
using System.Xml.Linq;
using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.Dictionaries;

public class ElectricalCabinetDo : BaseDo
{
    private string _Symbol;
    public string Symbol
    {
        get => _Symbol;
        set
        {
            _Symbol = value;
            RaisePropertyChanged(value);
        }
    }

    public override void SetObject(LookupModel lookup)
    {
        base.SetObject(lookup);
        Symbol = lookup.Property1;
    }
}
