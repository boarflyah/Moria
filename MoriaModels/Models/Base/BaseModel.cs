using MoriaBaseModels.Models;

namespace MoriaModels.Models.Base;
public abstract class BaseModel
{
    public int Id
    {
        get; set;
    }

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

    //TODO make this method abstract
    /// <summary>
    /// Projects object to it's lookup object version
    /// </summary>
    /// <returns><see cref="LookupModel"/> version of this object</returns>
    public virtual LookupModel GetLookupObject() => null;

}
