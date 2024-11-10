namespace MoriaBaseServices.Services
{
    public class WebAPIEndpointsProvider
    {
        #region controllers

        public const string Token = "token";

        #endregion

        #region endpoints

        #region Token

        public const string PostTokenPath = $"{Token}";

#if DEBUG

        public const string GetTokenGetTokenPath = $"{Token}/gettoken";

#endif

        #endregion

#if DEBUG
        public const string Test = "test";

        public const string GetTestPath = $"{Test}";

#endif

        #endregion
    }
}
