namespace MoriaBaseServices.Services;

public class WebAPIEndpointsProvider
{
    #region controllers

    public const string Token = "token";
    public const string Employee = "employee";
    public const string Lock = "lock";

    #endregion

    #region endpoints

    #region Token

    public const string PostTokenPath = $"{Token}";

#if DEBUG

    public const string GetTokenGetTokenPath = $"{Token}/gettoken";

#endif

    #endregion

    #region Lock

    /// <summary>
    /// Body: object of type LockHelper
    /// <para>Return bool</para>
    /// <para>Throws: </para>
    /// </summary>
    public const string PutLockPath = $"{Lock}/lock";
    /// <summary>
    /// Body: object of type LockHelper
    /// <para>Return bool</para>
    /// <para>Throws: </para>
    /// </summary>
    public const string PutUnlockPath = $"{Lock}/unlock";

    #endregion

    #region Employee

    /// <summary>
    /// Body: object of type UserCredentials
    /// <para>Return EmployeeDo</para>
    /// <para>Throws: </para>
    /// </summary>
    public const string PostLoginPath = $"{Employee}/login";

    public const string GetEmployeesPath = $"{Employee}";

    /// <summary>
    /// Parameter from path: employee.id
    /// </summary>
    public const string GetEmployeePath = $"{Employee}";

    /// <summary>
    /// Body: EmployeeDo
    /// <para>Return EmployeeDo</para>
    /// </summary>
    public const string PostEmployeePath = $"{Employee}";

    /// <summary>
    /// Body: EmployeeDo
    /// <para>Return EmployeeDo</para>
    /// </summary>
    public const string PutEmployeePath = $"{Employee}";

    /// <summary>
    /// Parameter from path: employee.id
    /// <para>Return bool</para>
    /// </summary>
    public const string DeleteEmployeePath = $"{Employee}";

    #endregion

#if DEBUG
    public const string Test = "test";

    public const string GetTestPath = $"{Test}";

#endif

    #endregion
}
