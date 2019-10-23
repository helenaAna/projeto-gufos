using System.Collections.Generic;
using System.Threading.Tasks;
using Gufos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gufos.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    [Produces ("application/json")]
    public class PresencaController : ControllerBase {
        GufosContext context = new GufosContext ();
/// <summary>
/// Método de consulta de presenças cadastradas
/// </summary>
/// <returns> Retorna as listas de presença já cadastradas</returns>
        [HttpGet]
        public async Task<ActionResult<List<Presenca>>> Get () {
            List<Presenca> listaDePresenca = await context.Presenca.Include (e => e.Evento).Include(u => u.Usuario).ToListAsync ();

            if (listaDePresenca == null) {
                return NotFound ();
            }
            return listaDePresenca;
        }
/// <summary>
/// 
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
        [HttpGet ("{id}")]
        public async Task<ActionResult<Presenca>> Get (int id) {
            Presenca presencaRetornada = await context.Presenca.FindAsync (id);
            if (presencaRetornada == null) {
                return NotFound ();
            }
            return presencaRetornada;
        }
/// <summary>
/// 
/// </summary>
/// <param name="presenca"></param>
/// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Presenca>> Post (Presenca presenca) {
            try {
                await context.Presenca.AddAsync (presenca);
                await context.SaveChangesAsync ();

            } catch (System.Exception) {

                throw;
            }
            return presenca;
        }
/// <summary>
/// 
/// </summary>
/// <param name="id"></param>
/// <param name="presenca"></param>
/// <returns></returns>
        [HttpPut ("{id}")]
        public async Task<ActionResult<Presenca>> Put (int id, Presenca presenca) {
            if (id != presenca.PresencaId) {
                return BadRequest ();
            }
            context.Entry (presenca).State = EntityState.Modified;

            try {
                await context.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                var PresencaValida = context.Presenca.FindAsync (id);
                if (PresencaValida == null) {
                    return NotFound ();
                }
            }

            await context.SaveChangesAsync ();

            return presenca;
        }
/// <summary>
/// 
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
        [HttpDelete ("{id}")]

        public async Task<ActionResult<Presenca>> Delete (int id) {
            Presenca presencaRetornada = await context.Presenca.FindAsync (id);
            if (presencaRetornada == null) {
                return NotFound ();
            }
            context.Presenca.Remove (presencaRetornada);
            await context.SaveChangesAsync ();

            return presencaRetornada;
        }
    }
}