using MoriaDesktopServices.Interfaces;
using MoriaModelsDo.Base;

namespace MoriaDesktop.Services;

public class NewObjectService : INewObjectService
{
    readonly IEnumerable<IDetailedWindow> _detailedWindows;

    public NewObjectService(IEnumerable<IDetailedWindow> detailedWindows)
    {
        _detailedWindows = detailedWindows;
    }

    public T GetNewObject<T>() where T : BaseDo, new() 
        => ResolveDetailedWindow<T>()?.ShowDialog<T>();

    IDetailedWindow ResolveDetailedWindow<T>()
        => _detailedWindows.FirstOrDefault(x => x.GetModelType() == typeof(T));

}
