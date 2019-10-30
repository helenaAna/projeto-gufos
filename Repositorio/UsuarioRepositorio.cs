using System.Collections.Generic;
using System.Threading.Tasks;
using Gufos.Interface;
using Gufos.Models;
using Microsoft.EntityFrameworkCore;

namespace Gufos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        GufosContext context = new GufosContext();
        public async Task<Usuario> Delete(Usuario usuarioRetornado)
        {
            context.Usuario.Remove(usuarioRetornado);
            await context.SaveChangesAsync ();
            return usuarioRetornado;
        }

        public async Task<List<Usuario>> Get()
        {
             List<Usuario> listaUsuario = await context.Usuario
                                        .Include(t => t.TipoUsuario)
                                        .ToListAsync();
            return listaUsuario;
        }

        public async Task<Usuario> Get(int id)
        {
            Usuario usuarioRetornado = await context.Usuario
                                        .Include(tu => tu.TipoUsuario)
                                        .FirstOrDefaultAsync(us => us.UsuarioId == id);
            
            return usuarioRetornado;
        }

        public async Task<Usuario> Post(Usuario usuario)
        {
            await context.Usuario.AddAsync (usuario);
            await context.SaveChangesAsync ();
            return usuario;
        }

        public async Task<Usuario> Put(Usuario usuario)
        {
            context.Entry (usuario).State = EntityState.Modified;
            await context.SaveChangesAsync ();
            return usuario;
        }
    }
}