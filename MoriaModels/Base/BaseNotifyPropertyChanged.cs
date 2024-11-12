using System.ComponentModel;

namespace MoriaModels.Base;
public abstract class BaseNotifyPropertyChanged: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void RaisePropertyChanged(string property, object oldValue = null, object newValue = null)
    {
        OnPropertyChanged(property, oldValue, newValue);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }

    protected virtual void OnPropertyChanged(string property, object oldValue = null, object newValue = null)
    {
    }

}
