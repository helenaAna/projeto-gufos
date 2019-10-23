using System.Collections.Generic;
using System.Threading.Tasks;
using Gufos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gufos.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    [Produces ("application/json")]

    public class UsuarioController : ControllerBase {
        GufosContext context = new GufosContext ();

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get () {
            List<Usuario> listaDeUsuario = await context.Usuario.Include (t => t.TipoUsuario).ToListAsync ();

            if (listaDeUsuario == null) {
                return NotFound ();
            }
            return listaDeUsuario;
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Usuario>> Get (int id) {
            Usuario usuarioRetornado = await context.Usuario.Include(tu => tu.TipoUsuario).FirstOrDefaultAsync(us => us.UsuarioId == id);

            if (usuarioRetornado == null) {
                return NotFound ();
            }
            return usuarioRetornado;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Post (Usuario usuario) {
            try {
                await context.Usuario.AddAsync (usuario);
                await context.SaveChangesAsync ();

            } catch (System.Exception) {

                throw;
            }
            return usuario;
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult<Usuario>> Put (int id, Usuario usuario) {
            if (id != usuario.UsuarioId) {
                return BadRequest ();
            }
            context.Entry (usuario).State = EntityState.Modified;

            try {
                await context.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                var usuarioValido = context.Usuario.FindAsync (id);
                if (usuarioValido == null) {
                    return NotFound ();
                }
            }

            await context.SaveChangesAsync ();

            return usuario;
        }

        [HttpDelete ("{id}")]

        public async Task<ActionResult<Usuario>> Delete (int id) {
            Usuario usuarioRetornado = await context.Usuario.FindAsync(id);
            if (usuarioRetornado == null) {
                return NotFound ();
            }
            context.Usuario.Remove (usuarioRetornado);
            await context.SaveChangesAsync ();

            return usuarioRetornado;
        }
    }
}