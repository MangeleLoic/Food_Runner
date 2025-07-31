using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization; //Import necessario per JsonStringEnumConverter

namespace FoodRunner.Utils
{
    public static class JsonFileHelper
    {
        // Questa classe fornisce metodi per caricare (deserializzare) e salvare (serializzare)
        // liste di oggetti in file JSON.
        // È una classe statica, quindi non può essere istanziata.

        // Configurazione delle opzioni per la serializzazione JSON
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            // Ignora la differenza tra maiuscole e minuscole nei nomi delle proprietà
            PropertyNameCaseInsensitive = true,

            // Indenta il JSON generato per renderlo più leggibile
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() } //per supportare enum come stringa
        };

        /// <summary>
        /// Carica una lista di oggetti di tipo T da un file JSON (deserializzazione).
        /// </summary>
        /// <typeparam name="T">Il tipo di oggetto contenuto nella lista</typeparam>
        /// <param name="filePath">Il percorso del file JSON</param>
        /// <returns>Una lista di oggetti di tipo T</returns>
        /// <remarks>
        /// Il parametro <c>T</c> indica che il metodo è generico e può essere utilizzato con qualsiasi tipo di oggetto,
        /// ad esempio List&lt;User&gt;, List&lt;Cliente&gt;, ecc.
        /// </remarks>
        public static List<T> LoadList<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                // Se il file non esiste, restituisce una lista vuota
                return new List<T>();
            }

            string json = File.ReadAllText(filePath); // Legge il contenuto del file JSON
            return JsonSerializer.Deserialize<List<T>>(json, options); // Lo deserializza in una lista di oggetti di tipo T
            // In alternativa, per evitare null:
            // return JsonSerializer.Deserialize<List<T>>(json, options) ?? new List<T>();
        }

        /// <summary>
        /// Salva una lista di oggetti di tipo T in un file JSON (serializzazione).
        /// </summary>
        /// <typeparam name="T">Il tipo di oggetto contenuto nella lista</typeparam>
        /// <param name="filePath">Il percorso del file JSON</param>
        /// <param name="list">La lista di oggetti da salvare</param>
        public static void SaveList<T>(string filePath, List<T> list)
        {
            string json = JsonSerializer.Serialize(list, options); // Serializza la lista in formato JSON
            File.WriteAllText(filePath, json); // Scrive il JSON nel file specificato
        }
    }
}
