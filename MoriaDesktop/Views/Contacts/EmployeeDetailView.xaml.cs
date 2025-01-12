﻿using System.Windows;
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

        var saveAndCloseButton = (Button)this.Template.FindName("SaveAndCloseButton", this);
        if (saveAndCloseButton != null)
        {
            saveAndCloseButton.Click -= SaveAndCloseButton_Click;
            saveAndCloseButton.Click += SaveAndCloseButton_Click;
        }
        PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;

        await (DataContext as BaseDetailViewModel)!.Load();
    }


    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        (DataContext as BaseDetailViewModel).SetHasObjectChanged(true);
        (DataContext as EmployeeDetailViewModel).PasswordChanged = true;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        await (DataContext as EmployeeDetailViewModel).SaveEmployee(PasswordBox.Password);
    }

    private async void SaveAndCloseButton_Click(object sender, RoutedEventArgs e)
    {
        await (DataContext as EmployeeDetailViewModel).SaveAndCloseEmployee(PasswordBox.Password);

    }

}
