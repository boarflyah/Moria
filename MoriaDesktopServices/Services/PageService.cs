using MoriaDesktopServices.Interfaces;

namespace MoriaDesktopServices.Services;
public class PageService: IPageService
{
    readonly Dictionary<Type, Type> RegisteredViews = new();

    protected void Register(Type viewModelType, Type viewType)
    {
        RegisteredViews.Add(viewModelType, viewType);
    }

    public Type GetViewType(Type viewModelType)
    {
        return RegisteredViews.GetValueOrDefault(viewModelType);
    }
}
