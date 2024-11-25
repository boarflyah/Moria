﻿using MoriaBaseServices.Args;

namespace MoriaDesktopServices.Interfaces;
public interface INavigationService
{
    void SetFrame(object frame);
    void NavigateTo<TViewModel>(bool clearNavigation, params object[] parameters);
    bool GoBack();
    bool CanGoBack { get; }
    bool IsOnGoBackNavigated { get; }
    event EventHandler<OnNavigatedEventArgs> OnNavigated;
}