namespace FoodRunner.Utils
{
    /// <summary>
    /// Classe di utilità per la generazione automatica di ID univoci.
    /// Funziona con qualsiasi oggetto che implementa l'interfaccia IIdentifiable.
    /// </summary>
    public static class IdGenerator
    {
        /// <summary>
        /// Calcola il prossimo ID disponibile per una lista di oggetti.
        /// </summary>
        /// <typeparam name="T">Tipo di oggetto che implementa l'interfaccia IIdentifiable.</typeparam>
        /// <param name="items">Lista degli oggetti già esistenti.</param>
        /// <returns>Un intero rappresentante il prossimo ID disponibile.</returns>
        public static int GenerateNextId<T>(List<T> items) where T : IIdentifiable
        {
            // Se la lista è vuota, inizia da 1
            if (items.Count == 0)
                return 1;

            // Altrimenti, cerca l'ID massimo tra gli oggetti e restituisce il successivo
            int maxId = 0;
            foreach (var item in items)
            {
                if (item.Id > maxId)
                    maxId = item.Id;
            }

            return maxId + 1;
        }
    }
}
