using MoriaBaseServices.Args;

namespace MoriaDesktopServices.Interfaces;
public interface INavigationService
{
    void SetFrame(object frame);
    void NavigateTo(Type viewModelType, bool clearNavigation, params object[] parameters);
    bool GoBack();
    bool CanGoBack { get; }
    bool IsOnGoBackNavigated { get; }
    event EventHandler<OnNavigatedEventArgs> OnNavigated;
}
