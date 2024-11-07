using System;
using System.Data.SqlClient;
using InsERT.Moria.Sfera;
using InsERT.Mox.Product;
using Microsoft.Extensions.Logging;
using MoriaDTObjects;
using MoriaServices.Interfaces;

namespace MoriaServices.Services
{
    public class MoriaHandlerService : IMoriaHandlerService
    {
        readonly ICredentialsService _credentialsService;
        readonly ILogger<MoriaHandlerService> _logger;

        public MoriaHandlerService(ICredentialsService credentialsService, ILogger<MoriaHandlerService> logger)
        {
            _credentialsService = credentialsService;
            _logger = logger;
        }

        public Uchwyt GetHandler()
        {
            string serverName = _credentialsService.GetServerName();
            string databaseName = _credentialsService.GetDatabaseName();

            bool windowsAuth = _credentialsService.GetIsWindowsAuthenticated();

            try
            {

                DanePolaczenia connection = null;
                if (!windowsAuth)
                {
                    string userName = _credentialsService.GetDatabaseUsername();
                    string password = _credentialsService.GetDatabasePassword();
                    connection = DanePolaczenia.Jawne(serverName, databaseName, windowsAuth, userName, password);
                }
                else
                    connection = DanePolaczenia.Jawne(serverName, databaseName, windowsAuth);
                MenedzerPolaczen mp = new MenedzerPolaczen();
                if (connection != null)
                    return mp.Polacz(connection, ProductId.Subiekt);
            }
            catch (InvalidOperationException ioe)
            {
                _logger.LogWarning($"Metoda łączaca z Subiektem - {ioe.Message}");
            }
            catch (SqlException se)
            {
                _logger.LogWarning($"Metoda łączaca z Subiektem - {se.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Metoda łączaca z Subiektem");
            }
            return null;
        }

        public bool Login(Uchwyt handler)
        {
            var isLoggedIn = handler.ZalogujOperatora(_credentialsService.GetMoriaUsername(), _credentialsService.GetMoriaPassword());

            if (!isLoggedIn)
                throw new MoriaException("Nie udało się zalogować do Sfery" ,_credentialsService.GetMoriaUsername());

            return isLoggedIn;
        }
    }
}
