namespace MoriaDesktop.Args;
public class InvokeViewEventArgs: EventArgs
{
    public InvokeViewEventArgs()
    {
        ReturnedValues = new();
    }

    /// <summary>
    /// result: true - accepted, null - cancelled, 
    /// </summary>
    public TaskCompletionSource<bool?> CompletionSource
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
