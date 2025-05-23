﻿using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Dictionary.DetailView;
using System.Windows.Controls;
namespace MoriaDesktop.Views.Dictionary;
public partial class WarehouseDetailView : Page
{
    public WarehouseDetailView(WarehouseDetailViewModel vm)
    {
        InitializeComponent();
        this.DataContext = vm;
    }

    private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        await(DataContext as BaseDetailViewModel)!.Load();
    }
}
