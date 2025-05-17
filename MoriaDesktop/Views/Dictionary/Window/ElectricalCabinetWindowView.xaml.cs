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
using System.Windows.Shapes;
using MoriaDesktop.Services.Interfaces;
using MoriaDesktop.ViewModels.Dictionary.DetailView;

namespace MoriaDesktop.Views.Dictionary.Window
{
    public partial class ElectricalCabinetWindowView : System.Windows.Window, IDetailedWindow
    {
        public ElectricalCabinetWindowView(ElectricalCabinetDetailViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
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

            var saveAndCloseButton = (Button)this.Template.FindName("SaveAndCloseButton", this);
            if (saveAndCloseButton != null)
            {
                saveAndCloseButton.Click -= SaveAndCloseButton_Click;
                saveAndCloseButton.Click += SaveAndCloseButton_Click;
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Cancelled = true;
            this.Hide();
        }

        private void SaveAndCloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        #endregion

        #region IDetailedWindow implementation

        public bool Cancelled
        {
            get; private set;
        }

        //public BaseDo ShowNewDialog()
        //{
        //    //TODO;
        //    return null;
        //}

        //public Type GetModelType() => (DataContext as BaseDetailViewModel).GetModelType();

        #endregion
    }
}
