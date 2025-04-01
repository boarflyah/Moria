namespace MoriaDesktopServices.Interfaces.ViewModels;
public interface INavigationAware
{
    void OnNavigatedTo(params object[] parameters);

    Task<bool> OnNavigatingFrom();
}
