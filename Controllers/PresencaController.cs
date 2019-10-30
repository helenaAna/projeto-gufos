using System.Collections.Generic;
using System.Threading.Tasks;
using Gufos.Models;
using Gufos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gufos.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    [Produces ("application/json")]
    public class PresencaController : ControllerBase {
        PresencaRepositorio repopositorio = new PresencaRepositorio();
/// <summary>
/// Método de consulta de presenças cadastradas
/// </summary>
/// <returns> Retorna as listas de presença já cadastradas</returns>
        [HttpGet]
        public async Task<ActionResult<List<Presenca>>> Get () {
            List<Presenca> listaDePresenca = await repopositorio.Get();

            if (listaDePresenca == null) {
                return NotFound ();
            }
            foreach (var item in listaDePresenca)
            {
                item.Evento.Presenca = null;
                item.Usuario.Presenca = null;
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
            Presenca presencaRetornada = await repopositorio.Get(id);
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
                await repopositorio.Post (presenca);
                
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
            try {
                await repopositorio.Put(presenca);
            } catch (DbUpdateConcurrencyException) 
            {
                var PresencaValida = repopositorio.Get(id);
                if (PresencaValida == null) 
                {
                    return NotFound ();
                }
                else
                {
                    throw;
                }
            }
            return presenca;
        }
/// <summary>
/// 
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
        [HttpDelete ("{id}")]

        public async Task<ActionResult<Presenca>> Delete (int id) {
            Presenca presencaRetornada = await repopositorio.Get(id);
            if (presencaRetornada == null) {
                return NotFound ();
            }
            await repopositorio.Delete(presencaRetornada);
            return presencaRetornada;
        }
    }
}