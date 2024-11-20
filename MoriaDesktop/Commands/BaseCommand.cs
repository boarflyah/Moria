using System.Windows.Input;

namespace MoriaDesktop.Commands;
public class BaseCommand<T> : ICommand
{
    Action<T> _TargetExecuteMethod;
    Func<bool> _TargetCanExecuteMethod;

    public BaseCommand(Action<T> targetExecuteMethod)
    {
        _TargetExecuteMethod = targetExecuteMethod;
    }

    public BaseCommand(Action<T> targetExecuteMethod, Func<bool> targetCanExecuteMethod) : this(targetExecuteMethod)
    {
        _TargetCanExecuteMethod = targetCanExecuteMethod;
    }

    public event EventHandler CanExecuteChanged = delegate { };

    public virtual bool CanExecute(object? parameter)
    {
        if (_TargetCanExecuteMethod != null)
            return _TargetCanExecuteMethod();

        if (_TargetExecuteMethod != null)
            return true;

        return false;
    }

    public virtual void Execute(object? parameter)
    {
        _TargetExecuteMethod?.Invoke((T)parameter);
    }

    public virtual void RaiseCanExecuteChanged()
    {
        CanExecuteChanged(this, EventArgs.Empty);
    }
}

public class BaseCommand : ICommand
{
    Action _TargetExecuteMethod;
    Func<bool> _TargetCanExecuteMethod;

    public BaseCommand(Action targetExecuteMethod)
    {
        _TargetExecuteMethod = targetExecuteMethod;
    }

    public BaseCommand(Action targetExecuteMethod, Func<bool> targetCanExecuteMethod) : this(targetExecuteMethod)
    {
        _TargetCanExecuteMethod = targetCanExecuteMethod;
    }

    public event EventHandler CanExecuteChanged = delegate { };

    public virtual bool CanExecute(object? parameter)
    {
        if (_TargetCanExecuteMethod != null)
            return _TargetCanExecuteMethod();

        if (_TargetExecuteMethod != null)
            return true;

        return false;
    }

    public virtual void Execute(object? parameter)
    {
        _TargetExecuteMethod?.Invoke();
    }

    public virtual void RaiseCanExecuteChanged()
    {
        CanExecuteChanged(this, EventArgs.Empty);
    }
}
