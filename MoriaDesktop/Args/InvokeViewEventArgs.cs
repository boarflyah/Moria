namespace MoriaDesktop.Args;
public class InvokeViewEventArgs: EventArgs
{
    public InvokeViewEventArgs()
    {
        ReturnedValues = new();
    }

    public TaskCompletionSource<bool> CompletionSource
    {
        get; set;
    }

    public Dictionary<string, object> ReturnedValues
    {
        get; set;
    }

    public object[] Parameters
    {
        get; set;
    }
}
