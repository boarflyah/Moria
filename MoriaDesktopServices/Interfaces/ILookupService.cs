using MoriaModelsDo.Base;

namespace MoriaDesktopServices.Interfaces;
public interface ILookupService
{
    Task<T> ShowLookup<T>(bool canAddNew = true) where T : BaseDo, new();
}
