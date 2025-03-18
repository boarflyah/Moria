﻿using MoriaBaseModels.Models;
using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.Contacts;
public class EmployeeDo: BaseDo
{
    string _FirstName;
    public string FirstName
    {
        get => _FirstName;
        set
        {
            _FirstName = value;
            RaisePropertyChanged(value);
        }
    }

    string _LastName;
    public string LastName
    {
        get => _LastName;
        set
        {
            _LastName = value;
            RaisePropertyChanged(value);
        }
    }

    string _UserName;
    public string Username
    {
        get => _UserName; 
        set
        {
            _UserName= value;
            RaisePropertyChanged(value);
        }
    }

    string? _Password;
    public string? Password
    {
        get => _Password;
        set
        {
            _Password = value;
            RaisePropertyChanged(value);
        }
    }

    string _PhoneNumber;
    public string PhoneNumber
    {
        get => _PhoneNumber;
        set
        {
         _PhoneNumber= value;
            RaisePropertyChanged(value);
        }
    }

    PositionDo _Position;
    public PositionDo Position
    {
        get => _Position;
        set
        {
            _Position = value;
            RaisePropertyChanged(value);
        }
    }

    bool _Admin;
    public bool Admin
    {
        get => _Admin;
        set
        {
            _Admin = value;
            RaisePropertyChanged(value);
        }
    }

    public string Token
    {
        get; set;
    }

    public DateTime ValidTo
    {
        get; set;
    }

    //public ObservableCollection<Permission> Permissions { get; set; } = new();

    public override void SetObject(LookupModel lookup)
    {
        base.SetObject(lookup);
        FirstName = lookup.Property1;
        LastName = lookup.Property2;
        Username = lookup.Property3;
    }
}
