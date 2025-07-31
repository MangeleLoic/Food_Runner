using FoodRunner.Models;
using FoodRunner.Utils;

namespace FoodRunner.Services
{
    public class UtenteService
    {
        //inizializzo una lista di utenti in memoria
        private readonly List<Utente> _utenti;

        public UtenteService()
        {
            //carico tutti i piatti dal file Json del menu
            _utenti = JsonFileHelper.LoadList<Utente>("Data/utenti.json");
        }

        public void Save()
        {
            JsonFileHelper.SaveList("Data/utenti.json", _utenti);
        }

        public List<Utente> GetAll()
        {
            return new List<Utente>(_utenti);// non è necessario ciclare per aggiungere ad una lista . è già una lista
        }

        public Utente GetById(int id)
        {
            //List<Utente> result = new List<Utente>();
            foreach (Utente utente in _utenti)
            {
                if (utente.Id == id)
                {
                    return utente;
                }
            }
            return null;
        }

        public Utente Add(Utente newUtente)
        {
            newUtente.Id = IdGenerator.GenerateNextId(_utenti);
            _utenti.Add(newUtente);
            LoggerHelper.Log($"Aggiunto un nuovo utente: {newUtente.Nome}");

            Save();
            return newUtente;
        }

        public bool Delete(int id)
        {
            Utente existing = null;
            foreach (Utente utente in _utenti)
            {
                if (utente.Id == id)
                {
                    existing = utente;
                    break;
                }
            }

            if (existing == null)
            {
                return false;
            }

            bool removed = _utenti.Remove(existing);

            if (removed)
            {
                LoggerHelper.Log($"Cancellato con successo l'utente con ID: {id}");
            }
            else
            {
                LoggerHelper.Log($"Tentativo di cancellazione fallito per l'utente con ID: {id}");
            }
            Save();
            return removed;
        }

        public bool Update(int id, Utente updatedUtente)
        {
            Utente existing = null;
            foreach (Utente utente in _utenti)
            {
                if (utente.Id == id)
                {
                    existing = utente;
                    break;
                }
            }
            if (existing == null)
            {
                return false;
            }

            existing.Nome = updatedUtente.Nome;
            existing.Email = updatedUtente.Email;
            existing.DataDiNascita = updatedUtente.DataDiNascita;
            existing.Indirizzo = updatedUtente.Indirizzo;

            LoggerHelper.Log($"Aggiornato con successo l'utente con ID: {id} ");
            Save(); 
            return true;
}

    }
}