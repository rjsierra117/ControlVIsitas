using APIControlVisitas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIControlVisitas.Controllers
{
    [Route("api/personas")]
    [ApiController]
    public class PersonaController : Controller
    {
        private readonly ControlVisitasContext _controlVisitasContext;

        public PersonaController(ControlVisitasContext controlVisitasContext)
        {
            _controlVisitasContext = controlVisitasContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> getTodas()
        {
            return await _controlVisitasContext.Personas.ToListAsync();
        }
        // GET: PersonaController/getCasa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> getPersona(int id)
        {
            var persona = await _controlVisitasContext.Personas.FindAsync(id);
            if (persona == null) return NotFound();
            return Ok(persona);
        }

        // POST: PersonaController/Create
        [HttpPost]
        public async Task<ActionResult<Persona>> CreatePersona(Persona persona)
        {
            try
            {
                _controlVisitasContext.Personas.Add(persona);
                await _controlVisitasContext.SaveChangesAsync();
                return Ok(CreatedAtAction("getPersona", new { id = persona.IdPersona }, persona));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: PersonaController/Update/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePersona(int id, Persona persona)
        {
            if (id != persona.IdPersona)
            {
                return BadRequest();
            }

            _controlVisitasContext.Entry(persona).State = EntityState.Modified;

            try
            {
                await _controlVisitasContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(CreatedAtAction("getPersona", new { id = persona.IdPersona }, persona));
        }


        // DELETE: PersonaController/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var persona = await _controlVisitasContext.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            _controlVisitasContext.Personas.Remove(persona);
            await _controlVisitasContext.SaveChangesAsync();

            return NoContent();
        }


        private bool PersonaExists(int id)
        {
            return _controlVisitasContext.Personas.Any(e => e.IdPersona == id);
        }
    }
}
