using MoriaBaseModels.Attributes;
using MoriaBaseModels.Models;
using MoriaModels.Attributes;
using MoriaModels.Models.Base;

namespace MoriaModels.Models.EntityPersonel;

[LookupHeaders(true, "Nazwa", true, "Kod")]
public class Position : BaseModel
{
    //public int Id { get; set; }

    [Searchable]
    public string Name { get; set; }

    [Searchable]
    public string Code { get; set; }

    public ICollection<Permission> Permissions { get; set; }


    public override LookupModel GetLookupObject() => new(Id, Name, Code);
}
