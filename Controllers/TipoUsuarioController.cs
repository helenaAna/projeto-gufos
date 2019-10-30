using System.Collections.Generic;
using System.Threading.Tasks;
using Gufos.Models;
using Gufos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Gufos.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    [Produces ("application/json")]
    public class TipoUsuarioController : ControllerBase 
    {
        TipoUsuarioRepositorio repositorio = new TipoUsuarioRepositorio();

        [HttpGet]
        public async Task<ActionResult<List<TipoUsuario>>> Get () {
            List<TipoUsuario> listaDeTipoUsuario = await repositorio.Get();

            if (listaDeTipoUsuario == null) {
                return NotFound ();
            }
            return listaDeTipoUsuario;
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<TipoUsuario>> Get (int id) {
            TipoUsuario tipoUsuarioRetornado = await repositorio.Get(id);

            if (tipoUsuarioRetornado == null) {
                return NotFound ();
            }
            return tipoUsuarioRetornado;
        }

        [HttpPost]
        public async Task<ActionResult<TipoUsuario>> Post (TipoUsuario tipoUsuario) {
            try {
                await repositorio.Post(tipoUsuario);
            } catch (System.Exception) {

                throw;
            }
            return tipoUsuario;
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult<TipoUsuario>> Put (int id, TipoUsuario tipoUsuario) {
            if (id != tipoUsuario.TipoUsuarioId) {
                return BadRequest ();
            }
            try {
                await repositorio.Put(tipoUsuario);
            } 
            catch (DbUpdateConcurrencyException) 
            {
                var tipoUsuarioValido = await repositorio.Get(id);
                if (tipoUsuarioValido == null) {
                    return NotFound ();
                }
                else
                {
                    throw;
                }
            }

            return tipoUsuario;
        }

        [HttpDelete ("{id}")]

        public async Task<ActionResult<TipoUsuario>> Delete (int id) {
            TipoUsuario tipoUsuarioRetornado = await repositorio.Get(id);
            if (tipoUsuarioRetornado == null) {
                return NotFound ();
            }
            await repositorio.Delete(tipoUsuarioRetornado);
            return tipoUsuarioRetornado;
        }
    }
}