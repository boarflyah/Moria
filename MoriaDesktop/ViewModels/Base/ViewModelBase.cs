using Microsoft.Extensions.Logging;
using MoriaModels.Base;

namespace MoriaDesktop.ViewModels.Base;

public abstract class ViewModelBase: BaseNotifyPropertyChanged
{
    protected readonly ILogger<ViewModelBase> _logger;

    protected ViewModelBase(ILogger<ViewModelBase> logger)
    {
        _logger = logger;
    }
}
