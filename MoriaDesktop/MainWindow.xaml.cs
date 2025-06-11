using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using MoriaDesktop.Models;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Contacts;
using MoriaDesktopServices.Interfaces;

namespace MoriaDesktop;

public partial class MainWindow : Window
{
    readonly INavigationService _navigationService;

    bool isMenuExpanded = true;
    readonly double menuExpandedWidth = 200;
    readonly double menuHiddenWidth = 56;
    readonly int menuAnimationDuration = 300;

    public MainWindow(MainWindowViewModel viewModel, INavigationService navigationService) : this()
    {
        DataContext = viewModel;
        (DataContext as MainWindowViewModel).OnConfirmationRequired += MainWindow_OnConfirmationRequired;
        _navigationService = navigationService;
    }

    public MainWindow()
    {
        InitializeComponent();
        MenuGrid.Width = menuExpandedWidth;
    }

    private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        //(DataContext as MainWindowViewModel)!.OnNavigationSelectionChanged(e.NewValue);
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        MaximizeWithoutCoveringTaskbar();
        (DataContext as MainWindowViewModel)!.NavigateToFirstView(); 
    }

    private void ExpandButton_Click(object sender, RoutedEventArgs e)
    {
        double from = 0,
            to = 0;
        if (isMenuExpanded)
            OnHidingMenu(ref from, ref to);
        else
            OnExpandingMenu(ref from, ref to);

        DoubleAnimation widthAnimation = new DoubleAnimation
        {
            From = from,
            To = to,
            Duration = new Duration(TimeSpan.FromMilliseconds(menuAnimationDuration)),
            AutoReverse = false,
            EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
        };

        MenuGrid.BeginAnimation(WidthProperty, widthAnimation);
    }

    private void OnHidingMenu(ref double from, ref double to)
    {
        from = menuExpandedWidth;
        to = menuHiddenWidth;
        TitleTextBlock.Visibility = Visibility.Hidden;
        NavigationTreeView.Visibility = Visibility.Hidden;
        FooterButtonsPanel.Orientation = System.Windows.Controls.Orientation.Vertical;
        isMenuExpanded = false;
    }

    private void OnExpandingMenu(ref double from, ref double to)
    {
        from = menuHiddenWidth;
        to = menuExpandedWidth;
        TitleTextBlock.Visibility = Visibility.Visible;
        NavigationTreeView.Visibility = (DataContext as MainWindowViewModel).IsLoggedIn ? Visibility.Visible : Visibility.Hidden;
        FooterButtonsPanel.Orientation = System.Windows.Controls.Orientation.Horizontal;
        isMenuExpanded = true;
    }

    private void HideButton_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private Rect previousBounds;
    private bool isRestored = false;

    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        if (!isRestored)
        {
            // Restore to smaller size
            previousBounds = new Rect(Left, Top, Width, Height);
            Width = 1000; // or your preferred small size
            Height = 700;
            Left = (SystemParameters.WorkArea.Width - Width) / 2;
            Top = (SystemParameters.WorkArea.Height - Height) / 2;
            isRestored = true;
        }
        else
        {
            MaximizeWithoutCoveringTaskbar();
        }
    }

    private void MaximizeButton_Click(object sender, RoutedEventArgs e)
    {
        //WindowState = WindowState.Maximized;
        MaximizeWithoutCoveringTaskbar();
    }

    private void MaximizeWithoutCoveringTaskbar()
    {
        if (!isRestored)
        {
            previousBounds = new Rect(Left, Top, Width, Height);
        }

        WindowState = WindowState.Normal;
        var workArea = SystemParameters.WorkArea;
        Left = workArea.Left;
        Top = workArea.Top;
        Width = workArea.Width;
        Height = workArea.Height;
        isRestored = false;
    }

    private async void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        if(await (DataContext as MainWindowViewModel).ConfirmClosing())
            Close();
    }

    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }

    private void NavigationTreeView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (e.OriginalSource is FrameworkElement fe && fe.DataContext is NavigationItem ni)
        {
            (DataContext as MainWindowViewModel)!.OnNavigationSelectionChanged(ni);
        }
    }

    private void myCalendar_SelectedDatesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        DateTime date = (sender as Calendar)?.SelectedDate ?? new DateTime();
        (DataContext as MainWindowViewModel)!.NavigateToCalendar(date);
    }

    private void MainWindow_OnConfirmationRequired(object sender, Args.ConfirmationEventArgs e)
    {
        var result = MessageBox.Show(e.Message, "Potwierdzenie", MessageBoxButton.YesNo);

        switch (result)
        {
            case MessageBoxResult.Yes:
                e.CompletionSource.SetResult(true);
                break;
            case MessageBoxResult.No:
                e.CompletionSource.SetResult(false);
                break;
            default:
                e.CompletionSource.SetResult(null);
                break;
        }
    }

    private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //if (sender is TabControl tc && tc.SelectedContent is Frame frame)
        if (sender is TabControl tc && tc.SelectedItem != null && tc.SelectedContent is Frame frame)
        {
            _navigationService.SetFrame(frame, tc.SelectedItem);
            (DataContext as MainWindowViewModel)?.OnNavigated(this, new MoriaBaseServices.Args.OnNavigatedEventArgs(frame.Content, null));
        }
    }
}