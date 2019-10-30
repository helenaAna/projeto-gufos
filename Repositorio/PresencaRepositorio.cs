using System.Collections.Generic;
using System.Threading.Tasks;
using Gufos.Interface;
using Gufos.Models;
using Microsoft.EntityFrameworkCore;

namespace Gufos.Repositorio
{
    public class PresencaRepositorio : IPresencaRepositorio
    {
        GufosContext context = new GufosContext ();
        public async Task<Presenca> Delete(Presenca presencaRetornada)
        {
            context.Presenca.Remove(presencaRetornada);
            await context.SaveChangesAsync ();
            return presencaRetornada;
        }

        public async Task<List<Presenca>> Get()
        {
            List<Presenca> listaPresenca = await context.Presenca
                                        .Include(e => e.Evento)
                                        .Include(u => u.Usuario)
                                        .ToListAsync();
            return listaPresenca;

        }

        public async Task<Presenca> Get(int id)
        {
            Presenca presencaRetornada = await context.Presenca
                                        .Include(e => e.Evento)
                                        .Include(u => u.Usuario)
                                        .FirstOrDefaultAsync(p => p.PresencaId == id);
            
            return presencaRetornada;
        }

        public async Task<Presenca> Post(Presenca presenca)
        {
            await context.Presenca.AddAsync (presenca);
            await context.SaveChangesAsync ();
            return presenca;
        }

        public async Task<Presenca> Put(Presenca presenca)
        {
            context.Entry (presenca).State = EntityState.Modified;
            await context.SaveChangesAsync ();
            return presenca;
        }
    }
}