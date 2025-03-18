﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MoriaDesktop.ViewModels.Base;

namespace MoriaDesktop.Views.Base;

public partial class CalendarView : Page
{
    public object GetViewModel() => DataContext;

    public CalendarView(CalendarViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }


    private async void Page_Loaed(object sender, RoutedEventArgs e)
    {
        await(DataContext as BaseDetailViewModel)!.Load();
    }
}
