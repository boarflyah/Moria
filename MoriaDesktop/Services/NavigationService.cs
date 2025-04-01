﻿using System.Windows.Controls;
using System.Windows.Navigation;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using MoriaBaseServices.Args;
using MoriaDesktop.Models;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Services;
public class NavigationService : INavigationService
{
    readonly IPageService _pageService;
    readonly IServiceScopeFactory _scopeFactory;
    Frame _frame;
    object[] _previousParameters;

    public NavigationService(IPageService pageService, IServiceScopeFactory scopeFactory)
    {
        _pageService = pageService;
        _scopeFactory = scopeFactory;
    }

    public bool CanGoBack => _frame.CanGoBack;
    public bool IsOnGoBackNavigated
    {
        get; set;
    }

    public event EventHandler<OnNavigatedEventArgs> OnNavigated;

    public void SetFrame(object frame)
    {
        _frame = (Frame)frame;
        _frame.Navigated += _frame_Navigated;
        _frame.Navigating += _frame_Navigating;
    }


    public bool GoBack()
    {
        if (_frame.NavigationService.CanGoBack)
        {
            IsOnGoBackNavigated = true;
            _frame.NavigationService.GoBack();
            return true;
        }
        return false;
    }
    public void NavigateTo(Type viewModelType, bool clearNavigation, params object[] parameters)
    {
        Type viewType = _pageService.GetViewType(viewModelType);
        if (viewType == null)
            throw new ArgumentException($"View not registered in PageService");

        IsOnGoBackNavigated = false;
        _frame.Tag = clearNavigation;
        _previousParameters = parameters;

        var pageUri = new Uri($"{viewType.FullName.Replace(".", "/")}.xaml", UriKind.Relative);

        using var scope = _scopeFactory.CreateScope();
        _frame.NavigationService.Navigate(scope.ServiceProvider.GetRequiredService(viewType), parameters);
    }

    private void _frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
    {
        if (sender is Frame frame)
        {
            var clearNavigation = (bool)frame.Tag;
            if (clearNavigation && frame.NavigationService != null)
                while (frame.NavigationService.RemoveBackEntry() != null)
                {
                }
            var navigationAware = (frame.Content as Page)?.DataContext as INavigationAware;
            OnNavigated?.Invoke(this, new OnNavigatedEventArgs(e.Content, e.ExtraData));

            if (navigationAware != null)
            {
                if (e.ExtraData is object[] array)
                    navigationAware.OnNavigatedTo(array);
                else
                    navigationAware.OnNavigatedTo(e.ExtraData);
            }
        }
    }

    private async void _frame_Navigating(object sender, NavigatingCancelEventArgs e)
    {
        if (sender is Frame frame)
        {
            if ((frame.Content as Page)?.DataContext is INavigationAware navigationAware)
                e.Cancel = !await navigationAware.OnNavigatingFrom();
        }
    }

}
