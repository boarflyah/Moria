﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.Orders;
using MoriaDesktopServices.Interfaces;
using MoriaDesktopServices.Interfaces.ViewModels;
using MoriaModelsDo.Models.Contacts;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;
using MoriaModelsDo.Models.Products;

namespace MoriaDesktop.Views.Orders;

public partial class OrderItemDetailView : Page, IViewModelContent
{
    readonly ILookupService _lookupService;

    public object GetViewModel() => DataContext;

    public OrderItemDetailView(ILookupService lookupService, OrderItemDetailViewModel vm)
    {
        InitializeComponent();
        _lookupService = lookupService;
        DataContext = vm;
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await (DataContext as BaseDetailViewModel).Load();
    }

    private static bool IsTextAllowed(string text)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(text, @"^[0-9]*(?:[\.\,][0-9]*)?$");
    }

    private void QuantityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !IsTextAllowed(e.Text);
    }

    private async void DesignerLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var employee = await _lookupService.ShowLookup<EmployeeDo>();
        if (employee != null)
            (DataContext as OrderItemDetailViewModel).Designer = employee;
    }

    private async void WarehouseLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var warehouse = await _lookupService.ShowLookup<WarehouseDo>();
        if (warehouse != null)
            (DataContext as OrderItemDetailViewModel).Warehouse = warehouse;
    }

    private async void DriveLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var drive = await _lookupService.ShowLookup<DriveDo>();
        if (drive != null)
            (DataContext as OrderItemDetailViewModel).Drive = drive;
    }

    private async void ComponentLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var component = await _lookupService.ShowLookup<ComponentDo>();
        if (component != null)
            (DataContext as OrderItemDetailViewModel).Component = component;
    }

    private async void ProductLookupObjectControl_OnLookupInvoked(object sender, EventArgs e)
    {
        var product = await _lookupService.ShowLookup<ProductDo>();
        if (product != null)
            (DataContext as OrderItemDetailViewModel).Product = product;
    }
}
