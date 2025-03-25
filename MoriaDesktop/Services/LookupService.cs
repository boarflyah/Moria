﻿using MoriaDesktop.Views.Base;
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

    public async Task<T> ShowLookup<T>(bool canAddNew = true) where T : BaseDo, new()
    {
        var wrapper = await _lookupWindow.ShowDialog<T>(canAddNew);
        if (wrapper.Selected != null)
            return wrapper.Selected;

        if (wrapper.CreateNew && canAddNew)
            return _newObjectService.GetNewObject<T>();

        return null;
    }
}
