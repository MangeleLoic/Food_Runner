using Microsoft.AspNetCore.Mvc;
using FoodRunner.Services;
using FoodRunner.Models;
using FoodRunner.DTOs;

namespace FoodRunner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdineController : ControllerBase
    {
        private readonly OrdineService _ordineService;

        public OrdineController(UtenteService utenteService, PiattoService piattoService)
        {
            _ordineService = new OrdineService(utenteService, piattoService);
        }

        // GET: api/ordine
        [HttpGet]
        public ActionResult<List<Ordine>> GetAll()
        {
            var ordini = _ordineService.GetAll();
            return Ok(ordini);
        }

        // GET: api/ordine/5
        [HttpGet("{id}")]
        public ActionResult<Ordine> GetById(int id)
        {
            var ordine = _ordineService.GetById(id);
            if (ordine == null)
                return NotFound($"Ordine con ID {id} non trovato.");
            return Ok(ordine);
        }

        // GET: api/ordine/cliente/id
        [HttpGet("cliente/{clienteId}")]
        public ActionResult<List<Ordine>> GetByClienteId(int clienteId)
        {
            var ordini = _ordineService.GetByClienteId(clienteId);
            return Ok(ordini);
        }

        // POST: api/ordine
        [HttpPost]
        public ActionResult<Ordine> CreaOrdine([FromBody] OrdineDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuovoOrdine = _ordineService.Add(dto.ClienteId, dto.PiattiOrdinatiIds);
            if (nuovoOrdine == null)
                return BadRequest("Errore nella creazione dell'ordine. Verifica cliente o piatti.");

            return CreatedAtAction(nameof(GetById), new { id = nuovoOrdine.Id }, nuovoOrdine);
        }

        // PUT: api/ordine/5/stato?nuovoStato=id
        [HttpPut("{id}/stato")]
        public ActionResult AggiornaStato(int id, [FromQuery] int nuovoStato)
        {
            if (!Enum.IsDefined(typeof(StatoOrdine), nuovoStato))
                return BadRequest("Stato ordine non valido.");

            bool success = _ordineService.UpdateStato(id, (StatoOrdine)nuovoStato);
            if (!success)
                return NotFound($"Ordine con ID {id} non trovato.");

            return NoContent();
        }

        // DELETE: api/ordine/5
        [HttpDelete("{id}")]
        public ActionResult DeleteOrdine(int id)
        {
            bool success = _ordineService.Delete(id);
            if (!success)
                return NotFound($"Ordine con ID {id} non trovato.");
            return NoContent();
        }

        // POST: api/ordine/5/aggiungipiatto/id
        [HttpPost("{ordineId}/aggiungipiatto/{piattoId}")]
        public ActionResult AddPiattoAOrdine(int ordineId, int piattoId)
        {
            var piatto = _ordineService.GetById(ordineId)?.PiattiOrdinati?.Find(p => p.Id == piattoId);
            if (piatto != null)
                return BadRequest("Il piatto è già presente nell’ordine.");

            var nuovoPiatto = new PiattoService().GetById(piattoId);
            if (nuovoPiatto == null)
                return NotFound($"Piatto con ID {piattoId} non trovato.");

            bool success = _ordineService.AddPiattoAOrdine(ordineId, nuovoPiatto);
            if (!success)
                return NotFound($"Ordine con ID {ordineId} non trovato.");

            return Ok("Piatto aggiunto con successo.");
        }

        // DELETE: api/ordine/5/rimuovipiatto/id
        [HttpDelete("{ordineId}/rimuovipiatto/{piattoId}")]
        public ActionResult RemovePiattoDaOrdine(int ordineId, int piattoId)
        {
            bool success = _ordineService.RimuoviPiattoDaOrdine(ordineId, piattoId);
            if (!success)
                return NotFound("Ordine o piatto non trovato.");

            return Ok("Piatto rimosso con successo.");
        }
    }
}
