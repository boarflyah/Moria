using MoriaDesktop.ViewModels.Base;
using MoriaModelsDo.Base;

namespace MoriaDesktop.Services.Interfaces;
public interface IDetailedWindow
{
    bool Cancelled
    {
        get;
    }

    object DataContext
    {
        get;
    }

    bool? ShowDialog();

    Type GetModelType() => (DataContext as BaseDetailViewModel)?.GetModelType();

    BaseDo ShowNewDialog()
    {
        ShowDialog();
        var result = (DataContext as BaseDetailViewModel)?.GetDo();
        (DataContext as BaseDetailViewModel).Clear();

        return result;
    }
}
