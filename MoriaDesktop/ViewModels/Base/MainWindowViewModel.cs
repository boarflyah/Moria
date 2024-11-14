using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MoriaDesktop.Models;

namespace MoriaDesktop.ViewModels.Base;

public class MainWindowViewModel : ViewModelBase
{
    #region DI properties



    #endregion

    public MainWindowViewModel(ILogger<MainWindowViewModel> logger) : base(logger)
    {
        Navigation = new();
        NavigationItem node1 = new()
        {
            Title = "CRM",
            Items = new()
        };

        NavigationItem node2 = new()
        {
            Title = "Produkcja",
            Items = new()
        };

        Navigation.Add(node1);
        Navigation.Add(node2);

        node2.Items.Add(new()
        {
            Title = "Zamówienia",
        });
        node2.Items.Add(new()
        {
            Title = "Napędy"
        });

        node1.Items.Add(new()
        {
            Title = "Pracownicy"
        });
        node1.Items.Add(new()
        {
            Title = "Podmioty"
        });

    }

    #region properties

    public ObservableCollection<NavigationItem> Navigation { get; set; }

    NavigationItem _SelectedItem;
    public NavigationItem SelectedItem
    {
        get => _SelectedItem;
        set
        {
            _SelectedItem = value;
            if (_SelectedItem != null)
                _SelectedItem.IsSelected = true;
            RaisePropertyChanged(nameof(SelectedItem), value);
        }
    }

    #endregion

    #region commands



    #endregion

    #region methods

    public void OnNavigationSelectionChanged(object obj)
    {
        foreach (var child in Navigation)
            RestartSelection(child);
        if (obj is NavigationItem ni && !ni.Items.Any())
            SelectedItem = ni;
        else
            SelectedItem = null;
    }

    bool RestartSelection(NavigationItem ni)
    {
        ni.IsSelected = false;
        foreach (var child in ni.Items)
        {
            RestartSelection(child);
        }
        return true;
    }

    #endregion

}
