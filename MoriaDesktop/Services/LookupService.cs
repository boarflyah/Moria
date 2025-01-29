using MoriaDesktop.Views.Base;
using MoriaDesktopServices.Interfaces;
using MoriaModelsDo.Base;

namespace MoriaDesktop.Services;
public class LookupService: ILookupService
{
    readonly LookupWindow _lookupWindow;
    readonly INewObjectService _newObjectService;

    public LookupService(LookupWindow lookupWindow, INewObjectService newObjectService)
    {
        _lookupWindow = lookupWindow;
        _newObjectService = newObjectService;
    }

    public T ShowLookup<T>() where T : BaseDo, new()
    {
        var wrapper = _lookupWindow.ShowDialog<T>();
        if (wrapper.Selected != null)
            return wrapper.Selected;

        if (wrapper.CreateNew)
            return _newObjectService.GetNewObject<T>();

        return null;
    }
}
