using System.ComponentModel.DataAnnotations;

namespace FoodRunner.DTOs
{
    public class OrdineDTO
    {
        [Required(ErrorMessage = "È richiesto l'ID del cliente.")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Devi selezionare almeno un piatto.")]
        public List<int> PiattiOrdinatiIds { get; set; }

        [Required]
        public DateTime DataOrdine { get; set; }
    }
}
