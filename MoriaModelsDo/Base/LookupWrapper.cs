namespace MoriaModelsDo.Base;
public class LookupWrapper<T> where T : BaseDo, new()
{
    public bool CreateNew
    {
        get; set;
    }

    public T Selected
    {
        get; set;
    }
}
