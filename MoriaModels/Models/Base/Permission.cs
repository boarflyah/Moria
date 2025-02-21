namespace MoriaModels.Models.Base;
public class Permission : BaseModel
{
    public bool CanRead { get; set; }

    public bool CanWrite { get; set; }

    public string DisplayName { get; set; } 

    public string PropertyName { get; set; }

    public int  PositionId { get; set; }

}