namespace MoriaBaseServices.Args;
public class OnNavigatedEventArgs: EventArgs
{
    public OnNavigatedEventArgs(object content, object parameters)
    {
        Content = content;
        Parameters = parameters;
    }

    public object Content
    {
        get; set;
    }
    public object Parameters
    {
        get; set;
    }
}
