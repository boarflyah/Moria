using MoriaModelsDo.Base;

namespace MoriaDesktopServices.Interfaces;
public interface INewObjectService
{
    T GetNewObject<T>() where T : BaseDo, new();
}
