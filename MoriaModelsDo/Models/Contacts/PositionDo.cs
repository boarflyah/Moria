﻿using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.Contacts;
public class PositionDo: BaseDo
{
    int _Id;
    public int Id
    {
        get => _Id;
        set
        {
            _Id= value;
            RaisePropertyChanged(value);
        }
    }

    string _Name;
    public string Name
    {
        get => _Name;
        set
        {
            _Name = value;
            RaisePropertyChanged(value);
        }
    }

    string _Code;
    public string Code
    {
        get => _Code;
        set
        {
            _Code = value;
            RaisePropertyChanged(value);
        }
    }
}
