using System;
using System.Text.RegularExpressions;

namespace FoodRunner.Utils
{
    public static class ValidationHelper
    {
        // Metodo che verifica che una stringa non sia nulla, vuota o composta solo da spazi
        public static bool IsStringValid(string? input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        // Metodo per verificare che un prezzo sia valido (non negativo)
        public static bool IsValidPrice(decimal price)
        {
            return price >= 0;
        }

        // Metodo per verificare che un CAP (Codice di Avviamento Postale) sia valido
        // Consideriamo un CAP italiano standard di 5 cifre
        public static bool IsValidCAP(string? cap)
        {
            if (string.IsNullOrWhiteSpace(cap))
                return false;

            return Regex.IsMatch(cap, @"^\d{5}$");
        }

        // Metodo per verificare che una stringa sia un indirizzo email valido
        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Semplice regex per email valida (non esaustiva ma efficace)
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            // oppure semplicemente(senza l'uso delle regex) 
            // return email.Contains("@");
        }

        // Metodo per verificare che un indirizzo sia valido:
        // controllo che città, via e CAP siano tutti non nulli, non vuoti e validi
        public static bool IsValidAddress(string? city, string? street, string? cap)
        {
            return IsStringValid(city) && IsStringValid(street) && IsValidCAP(cap);
        }
    }
}
