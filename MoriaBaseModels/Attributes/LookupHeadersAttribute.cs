using MoriaBaseModels.Models;

namespace MoriaBaseModels.Attributes;

/// <summary>
/// Use this attribute to define columns visibility and names for lookup window view
/// </summary>
[System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class LookupHeadersAttribute : Attribute
{
    readonly bool _property1Visibility;
    readonly bool _property2Visibility;
    readonly bool _property3Visibility;
    readonly bool _property4Visibility;
    readonly bool _property5Visibility;
    readonly string _property1Header;
    readonly string _property2Header;
    readonly string _property3Header;
    readonly string _property4Header;
    readonly string _property5Header;

    public LookupHeadersAttribute(bool property1Visibility, string property1Header, bool property2Visibility = false, string property2Header = "",
        bool property3Visibility = false, string property3Header = "", bool property4Visibility = false, string property4Header = "",
        bool property5Visibility = false, string property5Header = "")
    {
        _property1Visibility = property1Visibility;
        _property2Visibility = property2Visibility;
        _property3Visibility = property3Visibility;
        _property4Visibility = property4Visibility;
        _property5Visibility = property5Visibility;
        _property1Header = property1Header;
        _property2Header = property2Header;
        _property3Header = property3Header;
        _property4Header = property4Header;
        _property5Header = property5Header;
    }

    public LookupHeadersMetadata GetLookupHeadersMetadata() =>
        new(_property1Visibility, _property1Header, _property2Visibility, _property3Visibility, _property4Visibility, _property5Visibility,
            _property2Header, _property3Header, _property4Header, _property5Header);
}
