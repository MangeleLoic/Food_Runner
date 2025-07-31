using System.ComponentModel.DataAnnotations;

namespace FoodRunner.DTOs
{
    public class IndirizzoDTO
    {
        [Required(ErrorMessage = "La via è obbligatoria.")]
        public string Via { get; set; }

        [Range(1, 9999, ErrorMessage = "Il numero civico non è valido.")]
        public int Civico { get; set; }

        [Required(ErrorMessage = "La città è obbligatoria.")]
        public string Citta { get; set; }

        [RegularExpression(@"^\d{5}$", ErrorMessage = "Il CAP deve essere composto da 5 cifre.")]
        public string CAP { get; set; }
    }
}
