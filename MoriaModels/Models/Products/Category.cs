using MoriaModels.Models.Base;

namespace MoriaModels.Models.Products;

public class Category : BaseModel
{
    //public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
