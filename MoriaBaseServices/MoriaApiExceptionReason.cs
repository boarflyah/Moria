namespace MoriaBaseServices;
public enum MoriaApiExceptionReason
{
    Unknown = 0,
    DefaultExceptionCheckStatusCode = 10,
    ObjectIsLocked = 100,
    ObjectNotFound = 110,
    ValueIsNotUnique = 120,
    CreateCatalogError = 130,
    SubiektError = 140,
}
