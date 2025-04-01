namespace MoriaDesktop.Args;
public class ConfirmationEventArgs: EventArgs
{
    public ConfirmationEventArgs(string message)
    {
        Message = message;
    }

    public string Message
    {
        get; set;
    }

    /// <summary>
    /// result: true - accepted, null - cancelled, 
    /// </summary>
    public TaskCompletionSource<bool?> CompletionSource
    {
        get; set;
    }
}
