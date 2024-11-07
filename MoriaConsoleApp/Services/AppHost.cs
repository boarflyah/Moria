using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using MoriaDTObjects;
using MoriaServices.Interfaces;

namespace MoriaConsoleApp.Services
{
    public class AppHost
    {
        readonly ISalesOrderService _salesOrderService;
        readonly ILogger<AppHost> _logger;
        readonly ConsoleService _consoleService;

        public AppHost(ISalesOrderService salesOrderService, ConsoleService consoleService, ILogger<AppHost> logger)
        {
            _salesOrderService = salesOrderService;
            _logger = logger;
            _consoleService = consoleService;
        }

        public void Run(string[] args)
        {
            try
            {
                ConsoleKeyInfo pressed = default;
                do
                {
                    try
                    {
#if DEBUG
                        _consoleService.WriteLine("Pobierz listę zamówień (L) / Pobierz szczegóły zamówienia (S) / Test loggera (T) / Zakończ (K)");
#else
                        _consoleService.WriteLine("Pobierz listę zamówień (L) / Pobierz szczegóły zamówienia (S) / Zakończ (K)");
#endif
                        pressed = Console.ReadKey();
                        _consoleService.WriteLine();
                        if (pressed.Key == ConsoleKey.L)
                        {
                            DateTime ordersFrom = _consoleService.GetDate("Wskaż od jakiej daty(dd.mm.rrrr) pobrać zamówienia: ");
                            if (ordersFrom != DateTime.MaxValue)
                            {
                                _logger.LogInformation($"Lista zamówień od {ordersFrom.ToShortDateString()}");
                                var orders = _salesOrderService.GetSalesOrdersSimplified(ordersFrom);
                                _logger.LogInformation($"Pobranych zamówień {orders.Count}");
                                if (orders?.Any() == true)
                                {
                                    _consoleService.WriteLine($"Id\tSymbol\t\tData złożenia\t\t");
                                    foreach (var order in orders)
                                        _consoleService.WriteLine($"{order.Id}\t{order.Symbol}\t{order.DocumentDate.ToShortDateString()}\t");
                                    //TODO wypluc do pliku jsona z kolekcji pobranych zamówień, najpierw zapytac czy zapisywac do pliku

                                    _consoleService.WriteLine();
                                }
                                else
                                    _consoleService.WriteLine($"Brak zamówień od klientów z podanego zakresu: {ordersFrom.ToShortDateString()} - {DateTime.Today.ToShortDateString()}");
                            }
                        }
                        else if (pressed.Key == ConsoleKey.S)
                        {
                            var id = _consoleService.GetInt("Podaj identyfikator zamówienia: ");
                            if (id != int.MinValue)
                            {
                                _logger.LogInformation($"Wybrano zamówienie dla: {id}");
                                var doc = _salesOrderService.GetSalesOrder(id);
                                if (doc != null)
                                {
                                    _logger.LogInformation($"Znalezion zamówienie: {doc.Symbol}");
                                    _consoleService.WriteLine($"Id\tSymbol\t\tData złożenia\t\tZamawiajacy");
                                    _consoleService.WriteLine($"{doc.Id}\t{doc.Symbol}\t{doc.DocumentDate.ToShortDateString()}\t\t{doc.Client.ShortName}");
                                    _consoleService.WriteLine("\tLp\tProdukt\t\tIlość");
                                    foreach (var soi in doc.SalesOrderItems)
                                    {
                                        _consoleService.WriteLine($"\t{soi.Index}\t{soi.Product.Name}\t{soi.Quantity}");
                                        if (soi.Product?.Components?.Any() == true)
                                        {
                                            _consoleService.WriteLine("\t\tProdukt\t\tIlość");
                                            foreach (var comp in soi.Product.Components)
                                                _consoleService.WriteLine($"\t\t{comp.Product.Name}\t{comp.Quantity}");
                                        }
                                    }
                                    _consoleService.WriteLine();
                                }
                                else
                                    _logger.LogInformation($"Nie znaleziono dokumentu: {id}");
                            }
                        }
#if DEBUG
                        else if (pressed.Key == ConsoleKey.T)
                        {
                            var line = _consoleService.GetLine("Critical (C) / Warning (W) / Information (I) / Error (E)");
                            if (line.ToLower() == "c")
                                Log(isCritical: true);
                            else if (line.ToLower() == "w")
                                Log(isWarning: true);
                            else if (line.ToLower() == "i")
                                Log(isInformation: true);
                            else if (line.ToLower() == "e")
                                Log(isError: true);

                            _consoleService.WriteLine();
                        }
#endif
                        else if(pressed.Key == ConsoleKey.K)
                            _logger.LogInformation($"Koniec pracy {DateTime.Now.ToLongDateString()}");
                        else if (pressed.Key != ConsoleKey.K)
                            _consoleService.WriteLine($"Wybrano nieprawidłową opcję: {pressed.Key.ToString()}");

                    }
                    catch (MoriaException mex)
                    {
                        _logger.LogWarning($"Nazwa użytkownika: {mex.Username} - {mex.Message}");
                    }
                }
                while (pressed.Key != ConsoleKey.K);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"AppHost - {DateTime.Now}");
            }

            _consoleService.GetLine("Koniec..");
        }


#if DEBUG

        void Log(bool isCritical = false, bool isWarning = false, bool isInformation = false, bool isError = false, string message = "Log z AppHost")
        {
            if (isCritical)
                _logger.LogCritical(message);
            else if (isWarning)
                _logger.LogWarning(message);
            else if (isInformation)
                _logger.LogInformation(message);
            else
                _logger.LogError(message);
        }

#endif

    }
}
