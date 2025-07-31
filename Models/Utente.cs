using FoodRunner.Utils;
namespace FoodRunner.Models
{
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
}