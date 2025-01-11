using MoriaModelsDo.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoriaModelsDo.Models.Contacts;
public class ContactDo : BaseDo
{
    int _Id;
    public int Id
    {
        get => _Id;
        set
        {
            _Id = value;
            RaisePropertyChanged(value);
        }
    }

    string _ShortName;
    public string ShortName
    {
        get => _ShortName;
        set
        {
            _ShortName = value;
            RaisePropertyChanged(value);
        }
    }

    string _LongName;
    public string LongName
    {
        get => _LongName;
        set
        {
            _LongName = value;
            RaisePropertyChanged(value);
        }
    }

    string _Symbol;
    public string Symbol
    {
        get => _Symbol;
        set
        {
            _Symbol = value;
            RaisePropertyChanged(value);
        }
    }

}
