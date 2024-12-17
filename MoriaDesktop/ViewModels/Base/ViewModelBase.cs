using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using MoriaModels.Base;
using System.Windows.Input;

namespace MoriaDesktop.ViewModels.Base;

public abstract class ViewModelBase: BaseNotifyPropertyChanged
{
    protected readonly ILogger<ViewModelBase> _logger;

    protected ViewModelBase(ILogger<ViewModelBase> logger)
    {
        _logger = logger;

        SaveCommand = new RelayCommand(Save);
        SaveAndCloseCommand = new RelayCommand(SaveAndClose);
        CloseCommand = new RelayCommand(Close);
        EditCommand = new RelayCommand(Edit);
    }

    #region Commands

    public ICommand SaveCommand { get; }
    public ICommand SaveAndCloseCommand { get; }
    public ICommand CloseCommand { get; }
    public ICommand EditCommand { get; }

    private void Save()
    {
        // 
    }

    private void SaveAndClose()
    {
        // 
    }

    private void Close()
    {
        // 
    }

    private void Edit()
    {
        // 
    }

    #endregion
}
