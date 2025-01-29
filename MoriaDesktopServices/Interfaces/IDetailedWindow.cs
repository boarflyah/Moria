using MoriaModelsDo.Base;

namespace MoriaDesktopServices.Interfaces;
public interface IDetailedWindow
{
    Type GetModelType();
    T ShowDialog<T>() where T: BaseDo, new();
}
