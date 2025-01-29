using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.Products;
public class CategoryDo: BaseDo
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
}
