using System.ComponentModel.DataAnnotations;

namespace FoodRunner.DTOs
{
    public class PiattoDTO
    {
        [Required(ErrorMessage = "Il nome del piatto è obbligatorio.")]
        public string NomePiatto { get; set; }

        [Range(0.01, 100, ErrorMessage = "Il prezzo deve essere compreso tra 0.01 e 100.")]
        public decimal Prezzo { get; set; }

        public bool ContieneAllergeni { get; set; }

        public List<string> Ingredienti { get; set; }
    }
}
