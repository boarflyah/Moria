namespace MoriaBaseServices;

[Serializable]
public class MoriaAppException : Exception
{
    public MoriaAppException()
    {
    }
    public MoriaAppException(MoriaAppExceptionReason reason, string message) : base(message) 
    {
        Reason = reason;
    }
    public MoriaAppException(MoriaAppExceptionReason reason, string message, Exception inner) : base(message, inner)
    {
        Reason = reason;
    }

    public MoriaAppExceptionReason Reason
    {
        get; set;
    }
}
