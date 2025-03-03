using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MoriaDesktop.Models;

public class NavigationMessage<T> : ValueChangedMessage<T>
{
    public NavigationMessage(T value) : base(value)
    {
    }
}
