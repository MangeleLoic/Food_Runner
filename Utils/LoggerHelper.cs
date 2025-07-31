using System;

namespace FoodRunner.Utils
{
    // Classe statica responsabile della gestione centralizzata dei log dell'applicazione.
    // Consente di registrare messaggi sia su console che su file, utili per debug e tracciamento degli errori.
    public static class LoggerHelper
    {
        // Oggetto di sincronizzazione per garantire thread safety durante la scrittura su file.
        private static readonly object _lock = new();

        /// <summary>
        /// Registra un messaggio di log con timestamp, scrivendolo su console e su file.
        /// </summary>
        /// <param name="message">Il messaggio da registrare.</param>
        public static void Log(string message)
        {
            // Costruisce la riga di log includendo data e ora nel formato corretto.
            // 'MM' rappresenta il mese, 'mm' i minuti.
            string logLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";

            // Visualizza il messaggio di log nella console (utile durante lo sviluppo).
            Console.WriteLine(logLine);

            // Scrive la riga di log nel file 'log.txt'.
            // Il blocco lock impedisce accessi simultanei concorrenti da più thread.
            lock (_lock)
            {
                File.AppendAllText("log.txt", logLine + Environment.NewLine);
            }
        }
    }
}
