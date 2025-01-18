namespace MoriaModelsDo.Attributes;

/// <summary>
/// Attribute, that indicates we need to track property for ObjectChangedLogic in DetailViewModels
/// </summary>
[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
public sealed class ObjectChangedValidateAttribute : Attribute
{
    public ObjectChangedValidateAttribute()
    {
    }
}
