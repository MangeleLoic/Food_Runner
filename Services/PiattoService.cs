using FoodRunner.Models;
using FoodRunner.Utils;

namespace FoodRunner.Services
{
    public class PiattoService
    {
        //inizializzo una lista di piatti in memoria
        private readonly List<Piatto> _piatti;

        public PiattoService()
        {
            //carico tutti i piatti dal file Json del menu
            _piatti = JsonFileHelper.LoadList<Piatto>("Data/menu.json");
        }

        public void Save()
        {
            JsonFileHelper.SaveList("Data/menu.json", _piatti);
        }

        public List<Piatto> GetAll()
        {
            return new List<Piatto>(_piatti);// non è necessario ciclare per aggiungere ad una lista . è già una lista
        }

        public Piatto GetById(int id)
        {
            List<Piatto> result = new List<Piatto>();
            foreach (Piatto piatto in _piatti)
            {
                if (piatto.Id == id)
                {
                    return piatto;
                }
            }
            return null;
        }

        public Piatto Add(Piatto newPiatto)
        {
            newPiatto.Id = IdGenerator.GenerateNextId(_piatti);
            _piatti.Add(newPiatto);
            LoggerHelper.Log($"Aggiunto un nuovo piatto al menu: {newPiatto.NomePiatto}");
            Save();
            return newPiatto;
        }

        public bool Delete(int id)
        {
            Piatto existing = null;
            foreach (Piatto piatto in _piatti)
            {
                if (piatto.Id == id)
                {
                    existing = piatto;
                    break;
                }
            }

            if (existing == null)
            {
                return false;
            }

            bool removed = _piatti.Remove(existing);

            if (removed)
            {
                LoggerHelper.Log($"Cancellato con successo il piatto con ID: {id}");
            }
            else
            {
                LoggerHelper.Log($"Tentativo di cancellazione fallito per piatto con ID: {id}");
            }
            Save();
            return removed;
        }

        public bool Update(int id, Piatto updatedPiatto)
        {
            Piatto existing = null;
            foreach (Piatto piatto in _piatti)
            {
                if (piatto.Id == id)
                {
                    existing = piatto;
                    break;
                }
            }
            if (existing == null)
            {
                return false;
            }

            existing.NomePiatto = updatedPiatto.NomePiatto;
            existing.Prezzo = updatedPiatto.Prezzo;
            existing.ContieneAllergeni = updatedPiatto.ContieneAllergeni;
            existing.Ingredienti = updatedPiatto.Ingredienti;

            LoggerHelper.Log($"Aggiornato con successo il piatto con ID: {id} ");
            Save(); 
            return true;
}

    }
}