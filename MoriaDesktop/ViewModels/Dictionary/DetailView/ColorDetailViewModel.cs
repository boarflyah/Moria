using Microsoft.Extensions.Logging;
using MoriaDesktop.Services;
using MoriaDesktop.ViewModels.Base;
using MoriaModelsDo.Models.Dictionaries;
using MoriaModelsDo.Models.DriveComponents;
using System.Collections.ObjectModel;

namespace MoriaDesktop.ViewModels.Dictionary.DetailView;

public class ColorDetailViewModel : ViewModelBase
{
    public ColorDetailViewModel(ILogger<ViewModelBase> logger, AppStateService appStateService) : base(logger, appStateService)
    {
        Listmotos = new ObservableCollection<MotorDo>();
        Listmotos.Add(new MotorDo() { Name = "Name1" });
        Listmotos.Add(new MotorDo() { Name = "Name2" });
    }
    public ObservableCollection<MotorDo> Listmotos { get; set; }
    #region properties

    string _Code;
    public string Code
    {
        get => _Code;
        set
        {
            _Code = value;
            RaisePropertyChanged(value);
        }
    }

    string _Name;
    public string Name
    {
        get => _Name;
        set
        {
            _Name = value;
            RaisePropertyChanged(value);
        }
    }

    #endregion
}
