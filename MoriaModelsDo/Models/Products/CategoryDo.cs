﻿using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.Products;
public class CategoryDo: BaseDo
{

    private int _Id;
    public int Id
    {
        get => _Id;
        set
        {
            _Id = value;
            RaisePropertyChanged(value);
        }
    }

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
}