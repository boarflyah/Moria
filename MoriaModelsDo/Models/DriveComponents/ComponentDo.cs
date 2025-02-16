using MoriaBaseModels.Models;
using MoriaModelsDo.Base;
using MoriaModelsDo.Models.Products;

namespace MoriaModelsDo.Models.DriveComponents;
public class ComponentDo: BaseDo
{
    private string _ElectricalDescription;
    public string ElectricalDescription
    {
        get => _ElectricalDescription;
        set
        {
            _ElectricalDescription = value;
            RaisePropertyChanged(value);
        }
    }

    private ProductDo _Product;
    public ProductDo Product
    {
        get => _Product;
        set
        {
            _Product = value;
            RaisePropertyChanged(value);
        }
    }


    private ProductDo _ComponentProduct;
    public ProductDo ComponentProduct
    {
        get => _ComponentProduct;
        set
        {
            _ComponentProduct = value;
            RaisePropertyChanged(value);
        }
    }

    public override void SetObject(LookupModel lookup)
    {
        base.SetObject(lookup);
        ElectricalDescription = lookup.Property1;
    }
}
