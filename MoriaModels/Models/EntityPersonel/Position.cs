using MoriaModels.Models.Base;

namespace MoriaModels.Models.EntityPersonel;

public class Position : BaseModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
}
