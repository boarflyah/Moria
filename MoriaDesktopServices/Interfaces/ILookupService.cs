using MoriaModelsDo.Base;

namespace MoriaDesktopServices.Interfaces;
public interface ILookupService
{
    Task<T> ShowLookup<T>() where T : BaseDo, new();
}
