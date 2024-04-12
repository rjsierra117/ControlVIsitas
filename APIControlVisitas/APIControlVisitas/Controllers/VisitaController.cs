using APIControlVisitas.DTO;
using APIControlVisitas.Models;
using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace APIControlVisitas.Controllers
{
    [Route("api/visitas")]
    [ApiController]
    public class VisitaController : Controller
    {
        private readonly ControlVisitasContext _controlVisitasContext;

        public VisitaController(ControlVisitasContext controlVisitasContext)
        {
            _controlVisitasContext = controlVisitasContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitaPersona>>> getTodos()
        {
            var VisitaPersona = await (
               from visita in _controlVisitasContext.Visitas
               join resiente in _controlVisitasContext.Residentes
               on visita.IdResidente equals resiente.IdResidente
               join invitado in _controlVisitasContext.Invitados
               on visita.IdInvitado equals invitado.IdInvitado
               join re in _controlVisitasContext.Personas
               on resiente.IdPersona equals re.IdPersona
               join Inpersona in _controlVisitasContext.Personas
               on invitado.IdPersona equals Inpersona.IdPersona
               join casa in _controlVisitasContext.Casas
               on resiente.IdCasa equals casa.IdCasa
               select new
               {
                   visita.IdVisitas,
                   resiente.IdResidente,
                   Inpersona.Nombre,
                   Inpersona.Apellido,
                   casa.Modelo,
                   casa.Color,
                   Inpersona.NoIdentificacion,
                   visita.Estado,

               }).ToListAsync();

            if (VisitaPersona == null) return NoContent();

            return Ok(new { VisitaPersona });
        }
        // GET: InvitadoController/getInvitado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VisitaPersona>> getVisita(int id)
        {
            var visitaPersona = await (
               from visita in _controlVisitasContext.Visitas
               join resiente in _controlVisitasContext.Residentes
               on visita.IdResidente equals resiente.IdResidente
               join invitado in _controlVisitasContext.Invitados
               on visita.IdInvitado equals invitado.IdInvitado
               join re in _controlVisitasContext.Personas
               on resiente.IdPersona equals re.IdPersona
               join Inpersona in _controlVisitasContext.Personas
               on invitado.IdPersona equals Inpersona.IdPersona
               join casa in _controlVisitasContext.Casas
               on resiente.IdCasa equals casa.IdCasa
               select new 
               {
                   visita.IdVisitas,
                   resiente.IdResidente,
                   Inpersona.Nombre,
                   Inpersona.Apellido,
                   casa.Modelo,
                   casa.Color,
                   Inpersona.NoIdentificacion,
                   visita.Estado,
                   
               }).Where(x => x.IdVisitas == id).FirstOrDefaultAsync();

            if (visitaPersona == null) return NoContent();

            return Ok(new { visitaPersona });
        }

        // POST: InvitadoController/Create
        [HttpPost]
        public async Task<ActionResult<Invitado>> CreateVisita(Visita visita)
        {
            try
            {
                _controlVisitasContext.Visitas.Add(visita);
                await _controlVisitasContext.SaveChangesAsync();
                return Ok(CreatedAtAction("getVisita", new { id = visita.IdVisitas }, visita));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: InvitadoController/Update/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateVisita(int id, Visita visita)
        {
            if (id != visita.IdVisitas)
            {
                return BadRequest();
            }

            _controlVisitasContext.Entry(visita).State = EntityState.Modified;

            try
            {
                await _controlVisitasContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(CreatedAtAction("getVisita", new { id = visita.IdVisitas }, visita));
        }


        // DELETE: InvitadoController/Deletevisita/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisita(int id)
        {
            var visita = await _controlVisitasContext.Visitas.FindAsync(id);
            if (visita == null)
            {
                return NotFound();
            }

            _controlVisitasContext.Visitas.Remove(visita);
            await _controlVisitasContext.SaveChangesAsync();

            return NoContent();
        }


        private bool VisitaExists(int id)
        {
            return _controlVisitasContext.Visitas.Any(e => e.IdVisitas == id);
        }
    }
}
