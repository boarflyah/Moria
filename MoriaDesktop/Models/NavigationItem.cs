using System.Collections.ObjectModel;
using MoriaModels.Base;

namespace MoriaDesktop.Models;

public class NavigationItem: BaseNotifyPropertyChanged
{
    public NavigationItem()
    {
        Items = new();
    }

    public string Title { get; set; }

    public ObservableCollection<NavigationItem> Items { get; set; }
}
