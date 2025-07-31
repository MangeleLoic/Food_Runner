using System.ComponentModel.DataAnnotations;

namespace FoodRunner.DTOs
{
    public class UtenteDTO
    {
        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "L'email è obbligatoria.")]
        [EmailAddress(ErrorMessage = "Email non valida.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La data di nascita è obbligatoria.")]
        public DateTime DataDiNascita { get; set; }

        [Required(ErrorMessage = "L'indirizzo è obbligatorio.")]
        public IndirizzoDTO Indirizzo { get; set; }
    }
}
