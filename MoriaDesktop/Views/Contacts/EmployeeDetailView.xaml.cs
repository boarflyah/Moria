using System.Windows;
using System.Windows.Controls;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Contacts;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Views.Contacts;

public partial class EmployeeDetailView : Page, IViewModelContent
{
    public object GetViewModel() => DataContext;

    public EmployeeDetailView(EmployeeDetailViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        var saveButton = (Button)this.Template.FindName("SaveButton", this);
        if (saveButton != null)
        {
            saveButton.Click -= SaveButton_Click;
            saveButton.Click += SaveButton_Click;
        }
        PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;

        await (DataContext as BaseDetailViewModel)!.Load();
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        (DataContext as EmployeeDetailViewModel).PasswordChanged = true;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        await (DataContext as EmployeeDetailViewModel).SaveEmployee(PasswordBox.Password);
    }
}
