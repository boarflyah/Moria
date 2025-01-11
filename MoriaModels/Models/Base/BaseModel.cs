namespace MoriaModels.Models.Base;
public abstract class BaseModel
{
    public bool IsLocked
    {
        get; set;
    }

    public string LockedBy
    {
        get; set;
    }

    public string LastModified
    {
        get; set;
    }
}
