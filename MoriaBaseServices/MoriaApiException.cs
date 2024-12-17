using System.Net;
using System.Text.Json.Serialization;

namespace MoriaBaseServices;
public class MoriaApiException: Exception
{
    [JsonIgnore]
    public const int ApiExceptionThrownStatusCode = 700;

    public MoriaApiException(MoriaApiExceptionReason reason, int status)
    {
        Status = status;
        Reason = reason;
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

    [JsonIgnore]
    public HttpStatusCode? DefaultApiStatusCode => (HttpStatusCode?)Status;
}
