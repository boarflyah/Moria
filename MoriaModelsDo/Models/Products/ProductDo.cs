using MoriaBaseModels.Models;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;

namespace MoriaModelsDo.Models.Products;
public class ProductDo: BaseDo
{
    public ProductDo()
    {
        Components = new List<ComponentDo>();
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

    private bool _IsMainMachine;
    public bool IsMainMachine
    {
        get => _IsMainMachine;
        set
        {
            _IsMainMachine = value;
            RaisePropertyChanged(value);
        }
    }

    private string _SerialNumber;
    public string SerialNumber
    {
        get => _SerialNumber;
        set
        {
            _SerialNumber = value;
            RaisePropertyChanged(value);
        }
    }

    private CategoryDo _Category;
    public CategoryDo Category
    {
        get => _Category;
        set
        {
            _Category = value;
            RaisePropertyChanged(value);
        }
    }

    private SteelKindDo _SteelKind;
    public SteelKindDo SteelKind
    {
        get => _SteelKind;
        set
        {
            _SteelKind = value;
            RaisePropertyChanged(value);
        }
    }

    public IList<ComponentDo> Components
    {
        get; set;
    }

    public override void SetObject(LookupModel lookup)
    {
        base.SetObject(lookup);
        Symbol = lookup.Property1;
        Name = lookup.Property2;
    }
}
