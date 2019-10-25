using System.Collections.Generic;
using System.Threading.Tasks;
using Gufos.Interface;
using Gufos.Models;
using Microsoft.EntityFrameworkCore;

namespace Gufos.Repositorio {
    public class CategoriaRepositorio : ICategoriaRepositorio {
        GufosContext context = new GufosContext ();

        public async Task<Categoria> Delete (Categoria categoriaRetornada) {
            context.Categoria.Remove (categoriaRetornada);
            await context.SaveChangesAsync ();
            return categoriaRetornada;
        }

        public async Task<List<Categoria>> Get () {
            return await context.Categoria.ToListAsync ();
        }

        public async Task<Categoria> Get (int id) {
            return await context.Categoria.FindAsync (id);
        }

        public async Task<Categoria> Post (Categoria categoria) {
            await context.Categoria.AddAsync (categoria);
            await context.SaveChangesAsync ();
            return categoria;
        }

        public async Task<Categoria> Put (Categoria categoria) {
            context.Entry (categoria).State = EntityState.Modified;
            await context.SaveChangesAsync ();
            return categoria;
        }
    }
}