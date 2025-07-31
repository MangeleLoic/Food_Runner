using FoodRunner.Utils;
namespace FoodRunner.Models
{
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
}