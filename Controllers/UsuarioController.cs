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

    public class UsuarioController : ControllerBase {
        UsuarioRepositorio repositorio = new UsuarioRepositorio();
    
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get () {
            List<Usuario> listaDeUsuario = await repositorio.Get();
            
            if (listaDeUsuario == null) {
                return NotFound ();
            }
            return listaDeUsuario;
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Usuario>> Get (int id) {
            Usuario usuarioRetornado = await repositorio.Get(id);

            if (usuarioRetornado == null) {
                return NotFound ();
            }
            return usuarioRetornado;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Post (Usuario usuario) {
            try {
                await repositorio.Post(usuario);
        } catch (System.Exception) 
        {
            throw;
        }
            return usuario;
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult<Usuario>> Put (int id, Usuario usuario) {
            if (id != usuario.UsuarioId) {
                return BadRequest ();
            }
            try {
                await repositorio.Put(usuario);
            } 
            catch (DbUpdateConcurrencyException) 
            {
                var usuarioValido = await repositorio.Get(id);
                if (usuarioValido == null) {
                    return NotFound ();
                }
                else
                {
                    throw;
                }
            }

            return usuario;
        }

        [HttpDelete ("{id}")]

        public async Task<ActionResult<Usuario>> Delete (int id) {
            Usuario usuarioRetornado = await repositorio.Get(id);
            if (usuarioRetornado == null) {
                return NotFound ();
            }
            await repositorio.Delete(usuarioRetornado); 

            return usuarioRetornado;
        }
    }
}