using APIControlVisitas.DTO;
using APIControlVisitas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace APIControlVisitas.Controllers
{
    [Route("api/invitados")]
    [ApiController]
    public class InvitadoController : Controller
    {
        private readonly ControlVisitasContext _controlVisitasContext;

        public InvitadoController(ControlVisitasContext controlVisitasContext)
        {
            _controlVisitasContext = controlVisitasContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvitadoPesona>>> getTodos()
        {
            var invitadoPersona = await(
            from invitados in _controlVisitasContext.Invitados
            from persona in _controlVisitasContext.Personas
            where invitados.IdPersona == persona.IdPersona
            select new InvitadoPesona
            {
             IdInvitado = invitados.IdInvitado,
             Nombre = persona.Nombre,
             Apellido = persona.Apellido,
             NoIdentificacion = persona.NoIdentificacion,
             Estado = persona.Estado,
             Genero = persona.Genero

            }).ToListAsync();

            if(invitadoPersona == null) return NoContent();
                       
            return Ok(new {invitadoPersona});
        }
        // GET: InvitadoController/getInvitado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvitadoPesona>> getInvitado(int id)
        {
            var invitadoPersona = await (
            from invitados in _controlVisitasContext.Invitados
            from persona in _controlVisitasContext.Personas
            where invitados.IdPersona == persona.IdPersona
            select new InvitadoPesona
            {
                IdInvitado = invitados.IdInvitado,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                NoIdentificacion = persona.NoIdentificacion,
                Estado = persona.Estado,
                Genero = persona.Genero

            }).Where(x => x.IdInvitado == id).FirstOrDefaultAsync();

            if (invitadoPersona == null) return NoContent();

            return Ok(new { invitadoPersona });
        }

        // POST: InvitadoController/Create
        [HttpPost]
        public async Task<ActionResult<Invitado>> CreateInvitado(Invitado invitado)
        {
            try
            {
                _controlVisitasContext.Invitados.Add(invitado);
                await _controlVisitasContext.SaveChangesAsync();
                return Ok(CreatedAtAction("getInvitado", new { id = invitado.IdInvitado }, invitado));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: InvitadoController/Update/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateInvitado(int id, Invitado invitado)
        {
            if (id != invitado.IdInvitado)
            {
                return BadRequest();
            }

            _controlVisitasContext.Entry(invitado).State = EntityState.Modified;

            try
            {
                await _controlVisitasContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvitadoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(CreatedAtAction("getInvitado", new { id = invitado.IdInvitado }, invitado));
        }


        // DELETE: InvitadoController/DeleteInvitado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvitado(int id)
        {
            var invitado = await _controlVisitasContext.Invitados.FindAsync(id);
            if (invitado == null)
            {
                return NotFound();
            }

            _controlVisitasContext.Invitados.Remove(invitado);
            await _controlVisitasContext.SaveChangesAsync();

            return NoContent();
        }


        private bool InvitadoExists(int id)
        {
            return _controlVisitasContext.Invitados.Any(e => e.IdInvitado == id);
        }
    }
}
