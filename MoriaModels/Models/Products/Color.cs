using MoriaModels.Models.Base;

namespace MoriaModels.Models.Products;

public class Color: BaseModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
}
