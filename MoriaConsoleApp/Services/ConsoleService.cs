using System;

namespace MoriaConsoleApp.Services
{
    public class ConsoleService
    {
        public DateTime GetDate(string text)
        {
            var dateString = GetLine(text);
            DateTime date = DateTime.Today;
            if (DateTime.TryParse(dateString, out date))
                return date;
            else
               WriteLine($"Podano błędną datę: {dateString}.");

            return DateTime.MaxValue;
        }

        public int GetInt(string text)
        {
            var idString = GetLine(text);
            int id = int.MinValue;
            if (int.TryParse(idString, out id))
                return id;
            else
                WriteLine($"Podaną błędną liczbę całkowitą: {idString}");

            return int.MinValue;
        }

        public string GetLine(string text)
        {
            WriteLine(text);
            return Console.ReadLine();
        }

        public void WriteLine(string text = "") => Console.WriteLine(text);
    }
}
