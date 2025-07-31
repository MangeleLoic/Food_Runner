using FoodRunner.Utils;
namespace FoodRunner.Models
{
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
}
