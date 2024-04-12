using APIControlVisitas.DTO;
using APIControlVisitas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace APIControlVisitas.Controllers
{
    [Route("api/residentes")]
    [ApiController]
    public class ResidenteController : Controller
    {
        private readonly ControlVisitasContext _controlVisitasContext;

        public ResidenteController(ControlVisitasContext controlVisitasContext)
        {
            _controlVisitasContext = controlVisitasContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResidentePersona>>> getTodos()
        {
            var ResidentePersona = await (
            from  residente in _controlVisitasContext.Residentes
            from persona in _controlVisitasContext.Personas
            from casa in _controlVisitasContext.Casas
            where residente.IdPersona == persona.IdPersona
            where residente.IdCasa == casa.IdCasa
            select new ResidentePersona
            {
                IdResidente = residente.IdResidente,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                ModeloCasa = casa.Modelo,
                ColorCasa = casa.Color,
                NoIdentificacion = persona.NoIdentificacion,
                Estado = persona.Estado,
                Genero = persona.Genero

            }).ToListAsync();

            if (ResidentePersona == null) return NoContent();

            return Ok(new { ResidentePersona });
        }
        // GET: InvitadoController/getInvitado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResidentePersona>> getResidente(int id)
        {
            var residentePersona = await (
            from residente in _controlVisitasContext.Residentes
            from persona in _controlVisitasContext.Personas
            from casa in _controlVisitasContext.Casas
            where residente.IdPersona == persona.IdPersona
            where residente.IdCasa == casa.IdCasa
            select new ResidentePersona
            {
                IdResidente = residente.IdResidente,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                ModeloCasa = casa.Modelo,
                ColorCasa = casa.Color,
                NoIdentificacion = persona.NoIdentificacion,
                Estado = persona.Estado,
                Genero = persona.Genero

            }).Where(x => x.IdResidente == id ).FirstOrDefaultAsync();

            if (residentePersona == null) return NoContent();

            return Ok(new { residentePersona });
        }

        // POST: InvitadoController/Create
        [HttpPost]
        public async Task<ActionResult<Invitado>> CreateResidente(Residente residente)
        {
            try
            {
                _controlVisitasContext.Residentes.Add(residente);
                await _controlVisitasContext.SaveChangesAsync();
                return Ok(CreatedAtAction("getResidente", new { id = residente.IdResidente }, residente));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: InvitadoController/Update/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateResidente(int id, Residente residente)
        {
            if (id != residente.IdResidente)
            {
                return BadRequest();
            }

            _controlVisitasContext.Entry(residente).State = EntityState.Modified;

            try
            {
                await _controlVisitasContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResidenteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(CreatedAtAction("getResidente", new { id = residente.IdResidente }, residente));
        }


        // DELETE: InvitadoController/DeleteResidente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResidente(int id)
        {
            var residente = await _controlVisitasContext.Residentes.FindAsync(id);
            if (residente == null)
            {
                return NotFound();
            }

            _controlVisitasContext.Residentes.Remove(residente);
            await _controlVisitasContext.SaveChangesAsync();

            return NoContent();
        }


        private bool ResidenteExists(int id)
        {
            return _controlVisitasContext.Residentes.Any(e => e.IdResidente == id);
        }
    }
}
