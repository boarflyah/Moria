namespace MoriaBaseServices.Services;

public class WebAPIEndpointsProvider
{
    #region controllers

    public const string Token = "token";
    public const string Employee = "employee";

    #endregion

    #region endpoints

    #region Token

    public const string PostTokenPath = $"{Token}";

#if DEBUG

    public const string GetTokenGetTokenPath = $"{Token}/gettoken";

#endif

    #endregion

    #region Employee

    /// <summary>
    /// Body: object of type UserCredentials
    /// <para>Return EmployeeDo</para>
    /// <para>Throws: </para>
    /// </summary>
    public const string PostLoginPath = $"{Employee}/login";

    public const string GetEmployeesPath = $"{Employee}";

    #endregion

#if DEBUG
    public const string Test = "test";

    public const string GetTestPath = $"{Test}";

#endif

    #endregion
}
