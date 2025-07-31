namespace FoodRunner.Models
{
    public class Indirizzo
    {
        public string Via { get; set; }
        public int Civico { get; set; }
        public string Citta { get; set; }
        public string CAP { get; set; }

        public Indirizzo() { }

        public Indirizzo(string via, int civico, string citta, string cap)
        {
            Via = via;
            Civico = civico;
            Citta = citta;
            CAP = cap;
        }
    }
}
