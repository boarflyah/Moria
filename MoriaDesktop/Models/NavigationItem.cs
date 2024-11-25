using System.Collections.ObjectModel;
using MoriaDesktop.ViewModels.Base;
using MoriaModelsDo.Base;

namespace MoriaDesktop.Models;

public class NavigationItem: BaseNotifyPropertyChanged
{
    public NavigationItem()
    {
        Items = new();
    }

    string _Title;
    public string Title
    {
        get => _Title;
        set
        {
            _Title = value;
            RaisePropertyChanged(nameof(Title), value);
        }
    }

    bool _IsSelected;
    public bool IsSelected
    {
        get => _IsSelected;
        set
        {
            _IsSelected = value;
            RaisePropertyChanged(nameof(IsSelected), value);
        }
    }

    public Type ViewModelType
    {
        get;
        set;
    }

    public ViewModelBase ViewModelObject
    {
        get;
        set;
    }

    public ObservableCollection<NavigationItem> Items { get; set; }
}
