using System.ComponentModel;

namespace MoriaModelsDo.Base;
public abstract class BaseNotifyPropertyChanged: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void RaisePropertyChanged(string property, object newValue = null)
    {
        OnPropertyChanged(property, newValue);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }

    protected virtual void OnPropertyChanged(string property, object newValue = null)
    {
    }

}
