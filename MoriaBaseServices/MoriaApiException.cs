using System.Net;
using System.Text.Json.Serialization;

namespace MoriaBaseServices;
public class MoriaApiException: Exception
{
    [JsonIgnore]
    public const int ApiExceptionThrownStatusCode = 700;

    public MoriaApiException()
    {
    }

    public MoriaApiException(MoriaApiExceptionReason reason, int status)
    {
        Status = status;
        Reason = reason;
    }

    public MoriaApiException(MoriaApiExceptionReason reason, int status, string additionalMessage)
    {
        Status = status;
        Reason = reason;
        AdditionalMessage = additionalMessage;
    }

    /// <summary>
    /// Default custom message when <see cref="MoriaApiException"/> is thrown in Api with statuscode = 700
    /// </summary>
    public int Status
    {
        get; set;
    }

    public MoriaApiExceptionReason Reason
    {
        get; set;
    }

    public string AdditionalMessage
    {
        get; set;
    }

    [JsonIgnore]
    public HttpStatusCode? DefaultApiStatusCode => (HttpStatusCode?)Status;
}
