using Microsoft.AspNetCore.Mvc;
using FoodRunner.Models;
using FoodRunner.Services;
using FoodRunner.DTOs;

namespace FoodRunner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PiattoController : ControllerBase
    {
        private readonly PiattoService _service;

        public PiattoController(PiattoService service)
        {
            _service = service;
        }

        // GET: api/piatto
        [HttpGet]
        public ActionResult<List<Piatto>> GetAll()
        {
            var piatti = _service.GetAll();
            return Ok(piatti);
        }

        // GET: api/piatto/5
        [HttpGet("{id}")]
        public ActionResult<Piatto> GetById(int id)
        {
            var piatto = _service.GetById(id);
            if (piatto == null)
                return NotFound();

            return Ok(piatto);
        }

        // POST: api/piatto
        [HttpPost]
        public ActionResult<Piatto> AddPiatto([FromBody] PiattoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Conversione manuale da DTO a modello
            var nuovoPiatto = new Piatto
            {
                NomePiatto = dto.NomePiatto,
                Prezzo = dto.Prezzo,
                ContieneAllergeni = dto.ContieneAllergeni,
                Ingredienti = dto.Ingredienti
            };

            var aggiunto = _service.Add(nuovoPiatto);
            return CreatedAtAction(nameof(GetById), new { id = aggiunto.Id }, aggiunto);
        }

        // PUT: api/piatto/5
        [HttpPut("{id}")]
        public ActionResult UpdatePiatto(int id, [FromBody] PiattoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var piattoAggiornato = new Piatto
            {
                NomePiatto = dto.NomePiatto,
                Prezzo = dto.Prezzo,
                ContieneAllergeni = dto.ContieneAllergeni,
                Ingredienti = dto.Ingredienti
            };

            var successo = _service.Update(id, piattoAggiornato);
            if (!successo)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/piatto/5
        [HttpDelete("{id}")]
        public ActionResult DeletePiatto(int id)
        {
            var successo = _service.Delete(id);
            if (!successo)
                return NotFound();

            return NoContent();
        }
    }
}
