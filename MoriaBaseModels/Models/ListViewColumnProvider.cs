namespace MoriaBaseModels.Models;
public class ListViewColumnProvider
{
    public ListViewColumnProvider(string columnName, double columnWidth, int index, bool? ascending = null)
    {
        ColumnName = columnName;
        ColumnWidth = columnWidth;
        Ascending = ascending;
        Index = index;
    }

    public string ColumnName
    {
        get; set;
    }

    public double ColumnWidth
    {
        get; set;
    }

    public bool? Ascending
    {
        get; set;
    }

    public int Index
    {
        get; set;
    }
}
