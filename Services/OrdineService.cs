using FoodRunner.Models;
using FoodRunner.Utils;

namespace FoodRunner.Services
{
    public class OrdineService
    {
        // Lista degli ordini caricata da file
        private readonly List<Ordine> _ordini;

        // Servizi di supporto per controllare cliente e piatti
        private readonly UtenteService _utenteService;
        private readonly PiattoService _piattoService;

        // Costruttore: inizializza i servizi e carica gli ordini dal file JSON
        public OrdineService(UtenteService utenteService, PiattoService piattoService)
        {
            _utenteService = utenteService;
            _piattoService = piattoService;

            _ordini = JsonFileHelper.LoadList<Ordine>("Data/ordini.json");
        }

        // Metodo per salvare la lista degli ordini su file
        public void Save()
        {
            JsonFileHelper.SaveList("Data/ordini.json", _ordini);
        }

        // Restituisce una nuova lista contenente tutti gli ordini
        public List<Ordine> GetAll()
        {
            List<Ordine> result = new List<Ordine>();
            foreach (Ordine ordine in _ordini)
            {
                result.Add(ordine);
            }
            return result;
        }

        // Cerca un ordine per ID, restituendolo se trovato, altrimenti null
        public Ordine GetById(int id)
        {
            foreach (Ordine ordine in _ordini)
            {
                if (ordine.Id == id)
                {
                    return ordine;
                }
            }
            return null;
        }

        // Metodo per creare un nuovo ordine
        public Ordine Add(int utenteId, List<int> piattiIds)
        {
            // Recupera il cliente
            Utente utente = _utenteService.GetById(utenteId);
            if (utente == null)
            {
                LoggerHelper.Log("Errore: utente non trovato.");
                return null;
            }

            // Recupera i piatti validi
            List<Piatto> piatti = new List<Piatto>();
            for (int i = 0; i < piattiIds.Count; i++)
            {
                int idPiatto = piattiIds[i];
                Piatto piatto = _piattoService.GetById(idPiatto);
                if (piatto != null)
                {
                    piatti.Add(piatto);
                }
                else
                {
                    LoggerHelper.Log("Attenzione: piatto con ID " + idPiatto + " non trovato.");
                }
            }

            if (piatti.Count == 0)
            {
                LoggerHelper.Log("Errore: nessun piatto valido per l'ordine.");
                return null;
            }

            // Calcolo totale
            decimal totale = 0;
            for (int i = 0; i < piatti.Count; i++)
            {
                totale += piatti[i].Prezzo;
            }

            // Crea il nuovo ordine
            Ordine nuovoOrdine = new Ordine();
            nuovoOrdine.Id = IdGenerator.GenerateNextId(_ordini);
            nuovoOrdine.UtenteId = utenteId;
            nuovoOrdine.NomeUtente = utente.Nome;
            nuovoOrdine.PiattiOrdinati = piatti;
            nuovoOrdine.PrezzoOrdine = totale;
            nuovoOrdine.DataOrdine = DateTime.Now;
            nuovoOrdine.Stato = StatoOrdine.InPreparazione;

            // Aggiunge l'ordine alla lista
            _ordini.Add(nuovoOrdine);
            LoggerHelper.Log("Ordine creato con ID: " + nuovoOrdine.Id);
            Save();
            return nuovoOrdine;
        }

        // Elimina un ordine se esiste
        public bool Delete(int id)
        {
            Ordine daRimuovere = null;
            foreach (Ordine ordine in _ordini)
            {
                if (ordine.Id == id)
                {
                    daRimuovere = ordine;
                    break;
                }
            }

            if (daRimuovere == null)
            {
                LoggerHelper.Log("Errore: ordine non trovato.");
                return false;
            }

            bool rimosso = _ordini.Remove(daRimuovere);
            if (rimosso)
            {
                LoggerHelper.Log("Ordine con ID " + id + " rimosso con successo.");
                Save();
                return true;
            }
            else
            {
                LoggerHelper.Log("Errore nella rimozione dell’ordine.");
                return false;
            }
        }

        // Aggiorna lo stato di un ordine esistente
        public bool UpdateStato(int id, StatoOrdine nuovoStato)
        {
            Ordine ordine = GetById(id);
            if (ordine == null)
            {
                LoggerHelper.Log("Errore: ordine non trovato.");
                return false;
            }

            ordine.Stato = nuovoStato;
            LoggerHelper.Log("Stato ordine " + id + " aggiornato a " + nuovoStato.ToString());
            Save();
            return true;
        }

        // Restituisce tutti gli ordini di un determinato cliente
        public List<Ordine> GetByClienteId(int utenteId)
        {
            List<Ordine> ordiniCliente = new List<Ordine>();
            foreach (Ordine ordine in _ordini)
            {
                if (ordine.UtenteId == utenteId)
                {
                    ordiniCliente.Add(ordine);
                }
            }
            return ordiniCliente;
        }


        // Metodo per aggiungere un piatto a un ordine
        public bool AddPiattoAOrdine(int ordineId, Piatto nuovoPiatto)
        {
            Ordine ordine = GetById(ordineId);
            if (ordine == null)
                return false;

            ordine.PiattiOrdinati.Add(nuovoPiatto);

            // Ricalcolo del prezzo totale
            decimal totale = 0;
            foreach (Piatto p in ordine.PiattiOrdinati)
            {
                totale += p.Prezzo;
            }
            ordine.PrezzoOrdine = totale;

            LoggerHelper.Log($"Aggiunto piatto '{nuovoPiatto.NomePiatto}' all'ordine ID {ordineId}");
            Save();
            return true;
        }

        // Metodo per rimuovere un piatto da un ordine
        public bool RimuoviPiattoDaOrdine(int ordineId, int piattoId)
        {
            Ordine ordine = GetById(ordineId);
            if (ordine == null)
                return false;

            Piatto piattoDaRimuovere = null;
            foreach (Piatto p in ordine.PiattiOrdinati)
            {
                if (p.Id == piattoId)
                {
                    piattoDaRimuovere = p;
                    break;
                }
            }

            if (piattoDaRimuovere == null)
                return false;

            ordine.PiattiOrdinati.Remove(piattoDaRimuovere);

            // Ricalcolo del prezzo totale
            decimal totale = 0;
            foreach (Piatto p in ordine.PiattiOrdinati)
            {
                totale += p.Prezzo;
            }
            ordine.PrezzoOrdine = totale;

            LoggerHelper.Log($"Rimosso piatto ID {piattoId} dall'ordine ID {ordineId}");
            Save();
            return true;
        }
        
        
    }
}
