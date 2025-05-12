using System.Windows.Controls;
using MoriaBaseModels.Models;
using MoriaDesktop.ViewModels.Base;
using MoriaDesktopServices.Interfaces.ViewModels;

namespace MoriaDesktop.Interfaces;
public interface IListViewModelContent: IViewModelContent
{
    async Task SaveColumnsOrder()
    {
        if (GetViewModel() is BaseListViewModel vm)
        {
            var columns = new List<ListViewColumnProvider>();
            foreach (var column in DataGrid.Columns)
                columns.Add(new(column.SortMemberPath, column.ActualWidth, column.DisplayIndex, column.SortDirection == null ? null : column.SortDirection == System.ComponentModel.ListSortDirection.Ascending));

            await vm.UpdateListViewSetup(columns);
        }
    }

    async Task SetColumnsOrder()
    {
        if (GetViewModel() is BaseListViewModel vm)
        {
            var lvs = await vm.GetListViewSetup();
            if (lvs != null)
            {
                DataGrid.Items.SortDescriptions.Clear();

                foreach (var column in lvs.Columns)
                {

                    var dgColumn = DataGrid.Columns.FirstOrDefault(x => x.SortMemberPath == column.ColumnName);
                    if (dgColumn != null)
                    {
                        dgColumn.Width = new DataGridLength(column.ColumnWidth);
                        dgColumn.DisplayIndex = column.Index;
                        if (column.Ascending.HasValue)
                        {
                            DataGrid.Items.SortDescriptions.Add(new(column.ColumnName, column.Ascending.Value ? System.ComponentModel.ListSortDirection.Ascending : System.ComponentModel.ListSortDirection.Descending));
                            dgColumn.SortDirection = column.Ascending.Value ? System.ComponentModel.ListSortDirection.Ascending : System.ComponentModel.ListSortDirection.Descending;
                        }
                    }    
                }
            }
        }
    }

    DataGrid DataGrid
    {
        get;
    }
}
