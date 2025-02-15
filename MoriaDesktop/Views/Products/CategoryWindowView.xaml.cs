using System.Windows;
using System.Windows.Controls;
using MoriaDesktop.Services.Interfaces;
using MoriaDesktop.ViewModels.Products;

namespace MoriaDesktop.Views.Products;

public partial class CategoryWindowView : Window, IDetailedWindow
{
    public CategoryWindowView(CategoryDetailViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }

    private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
        {
            this.DragMove();
        }
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        var exitButton = (Button)this.Template.FindName("ExitButton", this);
        if (exitButton != null)
        {
            exitButton.Click -= ExitButton_Click;
            exitButton.Click += ExitButton_Click;
        }

        var saveAndCloseButton = (Button)this.Template.FindName("SaveAndCloseButton", this);
        if (saveAndCloseButton != null)
        {
            saveAndCloseButton.Click -= SaveAndCloseButton_Click;
            saveAndCloseButton.Click += SaveAndCloseButton_Click;
            ;
        }
    }

    private void SaveAndCloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Hide();
    }

    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        Cancelled = true;
        this.Hide();
    }

    #region IDetailedWindow implementation

    public bool Cancelled
    {
        get; private set;
    }

    #endregion
}
