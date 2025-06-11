using MoriaBaseServices.Args;

namespace MoriaDesktopServices.Interfaces;
public interface INavigationService
{
    void SetFrame(object frame);
    void SetFrame(object frame, object tabItem);
    void NavigateTo(Type viewModelType, bool clearNavigation, params object[] parameters);
    bool GoBack();
    void SetTabTitle(string text);
    bool CanGoBack { get; }
    bool IsOnGoBackNavigated { get; }
    event EventHandler<OnNavigatedEventArgs> OnNavigated;
}
