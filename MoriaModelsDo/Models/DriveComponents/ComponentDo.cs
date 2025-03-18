using MoriaBaseModels.Models;
using MoriaModelsDo.Attributes;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents.Relations;
using MoriaModelsDo.Models.Products;

namespace MoriaModelsDo.Models.DriveComponents;
public class ComponentDo: BaseDo
{
    private ProductDo _Product;
    [ObjectChangedValidate]
    public ProductDo Product
    {
        get => _Product;
        set
        {
            _Product = value;
            RaisePropertyChanged(value);
        }
    }

    private string _Name;
    [ObjectChangedValidate]
    public string Name
    {
        get => _Name;
        set
        {
            _Name = value;
            RaisePropertyChanged(value);
        }
    }

    private ProductDo _ComponentProduct;
    [ObjectChangedValidate]
    public ProductDo ComponentProduct
    {
        get => _ComponentProduct;
        set
        {
            _ComponentProduct = value;
            RaisePropertyChanged(value);
        }
    }


    private double _Quantity;
    [ObjectChangedValidate]
    public double Quantity
    {
        get => _Quantity;
        set
        {
            _Quantity = value;
            RaisePropertyChanged(value);
        }
    }


    private ColorDo _ComponentColor;
    [ObjectChangedValidate]
    public ColorDo ComponentColor
    {
        get => _ComponentColor;
        set
        {
            _ComponentColor = value;
            RaisePropertyChanged(value);
        }
    }

    public IList<DriveToComponentDo> Drives
    {
        get; set;
    } = new List<DriveToComponentDo>();

    public override void SetObject(LookupModel lookup)
    {
        base.SetObject(lookup);
        Name = lookup.Property1;
        //ComponentProduct = new()
        //{
        //    Name = lookup.Property2,
        //};
        //ComponentColor = new()
        //{
        //    Name = lookup.Property4
        //};
        double quantity;
        if (double.TryParse(lookup.Property2, out quantity))
            Quantity = quantity;
    }
}
