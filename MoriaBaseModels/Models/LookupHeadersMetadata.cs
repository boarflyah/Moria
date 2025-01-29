namespace MoriaBaseModels.Models;

/// <summary>
/// Metadata used for showing columns and headers names in lookup window view
/// </summary>
public class LookupHeadersMetadata
{
    public LookupHeadersMetadata(bool isProperty1Visible, string property1Header, bool isProperty2Visible = false, bool isProperty3Visible = false,
        bool isProperty4Visible = false, bool isProperty5Visible = false,
         string property2Header = "", string property3Header = "", string property4Header = "", string property5Header = "")
    {
        IsProperty1Visible = isProperty1Visible;
        IsProperty2Visible = isProperty2Visible;
        IsProperty3Visible = isProperty3Visible;
        IsProperty4Visible = isProperty4Visible;
        IsProperty5Visible = isProperty5Visible;
        Property1Header = property1Header;
        Property2Header = property2Header;
        Property3Header = property3Header;
        Property4Header = property4Header;
        Property5Header = property5Header;
    }

    public bool IsProperty1Visible
    {
        get; set;
    }
    public bool IsProperty2Visible
    {
        get; set;
    }
    public bool IsProperty3Visible
    {
        get; set;
    }
    public bool IsProperty4Visible
    {
        get; set;
    }
    public bool IsProperty5Visible
    {
        get; set;
    }
    public string Property1Header
    {
        get; set;
    }
    public string Property2Header
    {
        get; set;
    }
    public string Property3Header
    {
        get; set;
    }
    public string Property4Header
    {
        get; set;
    }
    public string Property5Header
    {
        get; set;
    }
}