**FoodRunner** è un'applicazione console scritta in **C#** per la gestione di un servizio di consegna pasti a domicilio. Il progetto nasce con l'obiettivo di simulare le funzionalità essenziali di un'app per ristoranti e delivery, offrendo un'esperienza completa lato backend.

##  Funzionalità principali

###  Gestione utenti
- Aggiunta di nuovi utenti con nome e ID univoco
- Ricerca di utenti tramite ID
- Validazione della presenza dell'utente prima di effettuare operazioni

```json
public class Utente :IIdentifiable
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataDiNascita { get; set; }
        public Indirizzo Indirizzo { get; set; }

        public Utente(string nome, string email, DateTime dataDiNascita, Indirizzo indirizzo)
        {
            Nome = nome;
            Email = email;
            DataDiNascita = dataDiNascita;
            Indirizzo = indirizzo;
        }
       
       public Utente() { }
    }
```

###  Gestione piatti e pizze
- Creazione di piatti e pizze con:
  - Nome
  - Prezzo
  - Lista ingredienti
  - Indicazione presenza allergeni
- Salvataggio automatico in file JSON
- Visualizzazione dell'intero menù disponibile

```json
  public class Piatto: IIdentifiable
    {
        public int Id { get; set; }
        public string NomePiatto { get; set; }
        public decimal Prezzo { get; set; }
        public bool ContieneAllergeni { get; set; }
        public List<string> Ingredienti { get; set; } 

        public Piatto(string nomePiatto, decimal prezzo, bool contieneAllergeni, List<string> ingredienti)
        {
            NomePiatto = nomePiatto;
            Prezzo = prezzo;
            ContieneAllergeni = contieneAllergeni;
            Ingredienti = ingredienti;
        }

        public Piatto() { }

    }
```

###  Creazione ordini
- Possibilità per un utente di selezionare più piatti
- Calcolo automatico del prezzo totale dell'ordine
- Assegnazione di data/ora e stato iniziale dell'ordine

```json
public class Ordine : IIdentifiable
    {
        public int Id { get; set; }
        public int UtenteId { get; set; }
        public string NomeUtente { get; set; } //provvisorio!!!
        public List<Piatto> PiattiOrdinati { get; set; } = new List<Piatto>();

        public decimal PrezzoOrdine { get; set; }
        // public string PrezzoOrdineFormattato => PrezzoOrdine.ToString("0.00") + " €"; //DA IMPLEMENTARE PIU TARDI!!!!!
        public DateTime DataOrdine { get; set; }

        public StatoOrdine Stato { get; set; }


        public Ordine() { }

        public Ordine(int utenteId, List<Piatto> piatti, decimal prezzoOrdine, DateTime dataOrdine, StatoOrdine stato)
        {
            UtenteId = utenteId;
            PiattiOrdinati = piatti;
            PrezzoOrdine = prezzoOrdine;
            DataOrdine = dataOrdine;
            Stato = stato;

        }

        public decimal CalcolaTotale()
        {
            return PiattiOrdinati.Sum(p => p.Prezzo);
        }



    }
```

###  Tracciamento ordini
- Ogni ordine ha uno stato associato:
  - `0` = InPreparazione
  - `1` = InConsegna
  - `2` = Consegnato
  - `3` = Annullato


###  Persistenza dati
- I dati vengono salvati in file `.json`:
  - `utenti.json` (lista dei clienti per il momento. In futuro ci sarà una distinzione tra clienti, rider e admin)
  - `menu.json` (lista con tutti i piatti disponibili nel ristorante)
  - `ordini.json` (lista degli ordini con una lista dei piatti selezionati, il prezzo totale e lo stato d'avanzamento)


###  Logging e validazioni
- Messaggi di log in console per ogni operazione critica(creazione/eliminazione/modifica di utenti/piatti/ordini)
- Logger personalizzato per avvisi e errori
- Validazione con attributi `DataAnnotations`


##  Esempi di dati

###  Esempio di piatto
```json
{
  "id": 1,
  "nomePiatto": "Lasagne al Ragu",
  "prezzo": 14.5,
  "contieneAllergeni": true,
  "ingredienti": ["pasta", "ragù", "besciamella"]
}
```

###  Esempio di ordine
```json
{
  "id": 2,
  "utenteId": 1,
  "nomeUtente": "Mario Rossi",
  "piattiOrdinati": [
    {"id": 3, "nomePiatto": "Pollo alla Cacciatora", "prezzo": 15.5}
  ],
  "prezzoOrdine": 15.5,
  "dataOrdine": "2025-07-30T12:34:56",
  "stato": 0
}
```

##  Implementazioni future
- Modello Ristorante
- Modello Rider
- Tipo di piatto nel modello "Piatto" (primo, secondo,piza ecc..)
- Interfaccia Consegna