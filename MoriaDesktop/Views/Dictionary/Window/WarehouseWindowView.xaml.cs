﻿
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using System.Windows;
using MoriaDesktop.ViewModels.Dictionary.DetailView;

namespace MoriaDesktop.Views.Dictionary.Window;

public partial class WarehouseWindowView : System.Windows.Window
{
    private WarehouseDetailViewModel detailViewModel;
    public WarehouseWindowView(WarehouseDetailViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;
        detailViewModel = viewModel;
    }

    #region BaseWindowFunctionality

    private void DragWindow(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
        {
            this.DragMove();
        }
    }

    private void WinowLoaed(object sender, RoutedEventArgs e)
    {
        var exitButton = (Button)this.Template.FindName("ExitButton", this);
        if (exitButton != null)
        {
            exitButton.Click -= ExitButton_Click;
            exitButton.Click += ExitButton_Click;
        }

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

        var minimalizeButton = (Button)this.Template.FindName("MinimalizeButton", this);
        if (minimalizeButton != null)
        {
            minimalizeButton.Click -= MinimalizeButton_Click;
            minimalizeButton.Click += MinimalizeButton_Click;
        }
    }

    private void MinimalizeButton_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        HandleCommand(nameof(detailViewModel.SaveCommand));
    }

    private void SaveAndCloseButton_Click(object sender, RoutedEventArgs e)
    {
        HandleCommand(nameof(detailViewModel.SaveAndCloseCommand));
    }

    private void HandleCommand(string commandName)
    {
        var command = detailViewModel.GetType()
            .GetProperty(commandName)?
            .GetValue(detailViewModel) as IRelayCommand;

        command?.Execute(null);
    }

    #endregion
}
