using MoriaBaseModels.Models;
using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.Products;
public class CategoryDo : BaseDo
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

    public override void SetObject(LookupModel lookup)
    {
        base.SetObject(lookup);
        Name = lookup.Property1;
    }
}
