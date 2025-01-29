using MoriaModelsDo.Base;

namespace MoriaDesktopServices.Interfaces;
public interface ILookupService
{
    T ShowLookup<T>() where T : BaseDo, new();
}
