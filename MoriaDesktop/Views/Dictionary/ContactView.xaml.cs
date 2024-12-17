﻿using MoriaDesktop.ViewModels.Dictionary;
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

namespace MoriaDesktop.Views.Dictionary;
public partial class ContactView : Page
{
    public ContactView(ContactViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}

