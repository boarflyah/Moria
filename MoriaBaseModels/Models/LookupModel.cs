namespace MoriaBaseModels.Models;

/// <summary>
/// Simplified object to show in lookup views
/// </summary>
public class LookupModel
{
    public LookupModel(int id, string property1, string property2 = "", string property3 = "", string property4 = "", string property5 = "")
    {
        Id = id;
        Property1 = property1;
        Property2 = property2;
        Property3 = property3;
        Property4 = property4;
        Property5 = property5;
    }

    public int Id
    {
        get; set;
    }

    public string Property1
    {
        get; set;
    }

    public string Property2
    {
        get; set;
    }

    public string Property3
    {
        get; set;
    }

    public string Property4
    {
        get; set;
    }

    public string Property5
    {
        get; set;
    }
}
