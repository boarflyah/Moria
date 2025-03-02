namespace MoriaDesktop.Attributes;

[System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
sealed class DefaultPropertyAttribute : Attribute
{
    public DefaultPropertyAttribute()
    {
    }
}
