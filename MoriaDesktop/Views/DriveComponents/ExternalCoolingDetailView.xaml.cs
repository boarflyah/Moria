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
using MoriaBaseServices;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktop.ViewModels.DriveComponents;

namespace MoriaDesktop.Views.DriveComponents
{
    /// <summary>
    /// Logika interakcji dla klasy ExternalCoolingDetailView.xaml
    /// </summary>
    public partial class ExternalCoolingDetailView : Page
    {
        private ExternalCoolingDetailViewModel vm;
        public ExternalCoolingDetailView(ExternalCoolingDetailViewModel externalCoolingDetailView)
        {
            InitializeComponent();
            vm = externalCoolingDetailView;
            DataContext = vm;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await (DataContext as BaseDetailViewModel)!.Load();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(sender as TextBox).Text.IsNumber(e.Text);
        }

        private void TextBox2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
