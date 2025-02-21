using MoriaBaseModels.Models;
using MoriaModelsDo.Base;

namespace MoriaModelsDo.Models.Contacts;
public class PositionDo: BaseDo
{

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

    public IList<PermissionDo>  Permissions {  get; set; } = new List<PermissionDo>();
    
    public override void SetObject(LookupModel lookup)
    {
        base.SetObject(lookup);
        Name = lookup.Property1;
        Code = lookup.Property2;
    }
}
