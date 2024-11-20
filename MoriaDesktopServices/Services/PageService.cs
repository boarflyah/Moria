using MoriaDesktopServices.Interfaces;

namespace MoriaDesktopServices.Services;
public class PageService: IPageService
{
    readonly Dictionary<Type, Type> RegisteredViews = new();

    protected void Register(Type viewModelType, Type viewType)
    {
        RegisteredViews.Add(viewModelType, viewType);
    }

    public Type GetViewType<TViewModel>()
    {
        return RegisteredViews.GetValueOrDefault(typeof(TViewModel));
    }
}
