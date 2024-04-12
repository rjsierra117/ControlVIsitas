using APIControlVisitas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIControlVisitas.Controllers
{
    [Route("api/casas")]
    [ApiController]
    public class CasaController : Controller
    {
        private readonly ControlVisitasContext _controlVisitasContext;
       
        public CasaController(ControlVisitasContext controlVisitasContext)
        {
            _controlVisitasContext = controlVisitasContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Casa>>> getTodas()
        {
            return await _controlVisitasContext.Casas.ToListAsync();
        }
        // GET: CasaController/getCasa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Casa>> getCasa(int id)
        {
            var casa = await _controlVisitasContext.Casas.FindAsync(id);
            if(casa == null) return NotFound();
            return Ok(casa) ;
        }

        // POST: CasaController/Create
        [HttpPost]
        public async Task<ActionResult<Casa>> CreateCasa(Casa casa)
        {
            try
            {
                _controlVisitasContext.Casas.Add(casa);
                await _controlVisitasContext.SaveChangesAsync();
                return Ok(CreatedAtAction("getCasa", new { id = casa.IdCasa }, casa));
            }
            catch(Exception ex) 
            {
                return BadRequest(ex);
            }
        }

        // PUT: CasaController/Update/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCasa(int id, Casa casa)
        {
            if (id != casa.IdCasa)
            {
                return BadRequest();
            }

            _controlVisitasContext.Entry(casa).State = EntityState.Modified;

            try
            {
                await _controlVisitasContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CasaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(CreatedAtAction("getCasa", new { id = casa.IdCasa }, casa)); 
        }


        // DELETE: CasaController/DeleteCasa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCasa(int id)
        {
            var casa = await _controlVisitasContext.Casas.FindAsync(id);
            if (casa == null)
            {
                return NotFound();
            }

            _controlVisitasContext.Casas.Remove(casa);
            await _controlVisitasContext.SaveChangesAsync();

            return NoContent();
        }


        private bool CasaExists(int id)
        {
            return _controlVisitasContext.Casas.Any(e => e.IdCasa == id);
        }
    }
}
