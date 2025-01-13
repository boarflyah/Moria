﻿using MoriaDesktop.ViewModels.Dictionary.ListView;
using System;
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

namespace MoriaDesktop.Views.Dictionary.ListView;

public partial class SteelKindListView : Page
{
    public SteelKindListView(SteelKindListViewModel listViewModel) : this()
    {
        DataContext = listViewModel;
    }

    public SteelKindListView()
    {
        InitializeComponent();
    }

    private void SteelKindsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {

    }
}
