using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MoriaModelsDo.Base;
public abstract class BaseNotifyPropertyChanged: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

   
    protected void RaisePropertyChanged(object newValue = null, [CallerMemberName] string property = default)
    {
        OnPropertyChanged(property, newValue);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }

    protected virtual void OnPropertyChanged(string property, object newValue = null)
    {
    }

}
