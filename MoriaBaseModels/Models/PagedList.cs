namespace MoriaBaseModels.Models;
public class PagedList<T> : List<T>
{
    public PagedList(IEnumerable<T> source, int lastId, bool hasNext)
    {
        AddRange(source);
        LastId = lastId;
        HasNext = hasNext;
    }

    public PagedList(IEnumerable<T> source, int lastId, bool hasNext, LookupHeadersMetadata lookupHeadersMetadata)
    {
        AddRange(source);
        LastId = lastId;
        HasNext = hasNext;
        LookupMetadata = lookupHeadersMetadata;
    }

    public int LastId
    {
        get; set;
    }

    public bool HasNext
    {
        get; set;
    }

    public LookupHeadersMetadata LookupMetadata
    {
        get; set;
    }
}
