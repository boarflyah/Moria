using System.Net;
using System.Text.Json.Serialization;

namespace MoriaBaseServices;
public class ApiException: Exception
{
    [JsonIgnore]
    public const int ApiExceptionThrownStatusCode = 700;

    public ApiException(ApiExceptionReason reason, int status)
    {
        Status = status;
        Reason = reason;
    }

    /// <summary>
    /// Default custom message when <see cref="ApiException"/> is thrown in Api with statuscode = 700
    /// </summary>
    public int Status
    {
        get; set;
    }

    public ApiExceptionReason Reason
    {
        get; set;
    }

    [JsonIgnore]
    public HttpStatusCode? DefaultApiStatusCode => (HttpStatusCode?)Status;
}
