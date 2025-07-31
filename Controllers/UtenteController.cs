using FoodRunner.DTOs;
using FoodRunner.Models;
using FoodRunner.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodRunner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UtenteController : ControllerBase
    {
        private readonly UtenteService _utenteService;

        public UtenteController()
        {
            _utenteService = new UtenteService();
        }

        // GET: api/utente
        [HttpGet]
        public ActionResult<List<Utente>> GetAll()
        {
            return Ok(_utenteService.GetAll());
        }

        // GET: api/utente/5
        [HttpGet("{id}")]
        public ActionResult<Utente> GetById(int id)
        {
            var utente = _utenteService.GetById(id);
            if (utente == null)
                return NotFound($"Nessun utente trovato con ID {id}");
            return Ok(utente);
        }

        // POST: api/utente
        [HttpPost]
        public ActionResult<Utente> AddUtente([FromBody] UtenteDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuovoUtente = new Utente
            {
                Nome = dto.Nome,
                Email = dto.Email,
                DataDiNascita = dto.DataDiNascita,
                Indirizzo = new Indirizzo
                {
                    Via = dto.Indirizzo.Via,
                    Civico = dto.Indirizzo.Civico,
                    Citta = dto.Indirizzo.Citta,
                    CAP = dto.Indirizzo.CAP
                }
            };

            var aggiunto = _utenteService.Add(nuovoUtente);
            return CreatedAtAction(nameof(GetById), new { id = aggiunto.Id }, aggiunto);
        }

        // PUT: api/utente/5
        [HttpPut("{id}")]
        public ActionResult UpdateUtente(int id, [FromBody] UtenteDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var aggiornato = new Utente
            {
                Nome = dto.Nome,
                Email = dto.Email,
                DataDiNascita = dto.DataDiNascita,
                Indirizzo = new Indirizzo
                {
                    Via = dto.Indirizzo.Via,
                    Civico = dto.Indirizzo.Civico,
                    Citta = dto.Indirizzo.Citta,
                    CAP = dto.Indirizzo.CAP
                }
            };

            var success = _utenteService.Update(id, aggiornato);
            if (!success)
                return NotFound($"Nessun utente trovato con ID {id}");

            return NoContent();
        }

        // DELETE: api/utente/5
        [HttpDelete("{id}")]
        public ActionResult DeleteUtente(int id)
        {
            var success = _utenteService.Delete(id);
            if (!success)
                return NotFound($"Nessun utente trovato con ID {id}");

            return NoContent();
        }
    }
}
