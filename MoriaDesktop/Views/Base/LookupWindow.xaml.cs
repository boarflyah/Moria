using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using MoriaBaseModels.Models;
using MoriaModelsDo.Base;

namespace MoriaDesktop.Views.Base;

public partial class LookupWindow : Window
{
    public LookupWindow(LookupWindowViewModel vm)
    {
        DataContext = vm;
        InitializeComponent();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        CreateColumns();
    }

    private void LookupDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (e.OriginalSource is FrameworkElement fe && fe.DataContext is LookupModel lm)
        {
            (DataContext as LookupWindowViewModel).SetSelected(lm, false);
            Hide();
        }
        else
            (DataContext as LookupWindowViewModel).SetSelected(null, false);
    }

    void CreateColumns()
    {
        var lh = (DataContext as LookupWindowViewModel).LookupMetadata;
        if (lh != null)
        {
            LookupDataGrid.Columns.Clear();
            if (lh.IsProperty1Visible)
                LookupDataGrid.Columns.Add(CreateColumn(lh.Property1Header, nameof(LookupModel.Property1)));
            if (lh.IsProperty2Visible)
                LookupDataGrid.Columns.Add(CreateColumn(lh.Property2Header, nameof(LookupModel.Property2)));
            if (lh.IsProperty3Visible)
                LookupDataGrid.Columns.Add(CreateColumn(lh.Property3Header, nameof(LookupModel.Property3)));
            if (lh.IsProperty4Visible)
                LookupDataGrid.Columns.Add(CreateColumn(lh.Property4Header, nameof(LookupModel.Property4)));
            if (lh.IsProperty5Visible)
                LookupDataGrid.Columns.Add(CreateColumn(lh.Property5Header, nameof(LookupModel.Property5)));
        }
    }

    DataGridTextColumn CreateColumn(string header, string path)
    {
        return new()
        {
            Header = header,
            Binding = new Binding(path),
            Width = new(1, DataGridLengthUnitType.Star)
        };
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        (DataContext as LookupWindowViewModel).SetSelected(null, false);
        Hide();
    }

    private void NewButton_Click(object sender, RoutedEventArgs e)
    {
        (DataContext as LookupWindowViewModel).SetSelected(null, true);
        Hide();
    }

    public async Task<LookupWrapper<T>> ShowDialog<T>() where T : BaseDo, new()
    {
        (DataContext as LookupWindowViewModel).SetType<T>();

        await (DataContext as LookupWindowViewModel).LoadNext();
        this.ShowDialog();

        return (DataContext as LookupWindowViewModel).OnClosed<T>();
    }

    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
        {
            this.DragMove();
        }
    }
}
