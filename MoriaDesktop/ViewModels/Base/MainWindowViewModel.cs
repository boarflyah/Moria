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
    }

    #region properties


    public ObservableCollection<NavigationItem> Navigation { get; set; }

    #endregion

    #region commands



    #endregion

    #region methods



    #endregion

}
