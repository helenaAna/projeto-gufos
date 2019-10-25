using System.Collections.Generic;
using System.Threading.Tasks;
using Gufos.Interface;
using Gufos.Models;
using Microsoft.EntityFrameworkCore;

namespace Gufos.Repositorio
{
    public class EventoRepositorio : IEventoRepositorio
    {
         GufosContext context = new GufosContext ();
        public async Task<Evento> Delete(Evento eventoRetornado)
        {
            context.Evento.Remove(eventoRetornado);
            await context.SaveChangesAsync ();
            return eventoRetornado;
        }

        public async Task<List<Evento>> Get()
        {
            List<Evento> listaDeEvento = await context.Evento
                                        .Include(c => c.Categoria)
                                        .Include(l => l.Localizacao)
                                        .ToListAsync();
            return listaDeEvento;
        }

        public async Task<Evento> Get(int id)
        {
            Evento eventoRetornado = await context.Evento
                                        .Include(c => c.Categoria)
                                        .Include(l => l.Localizacao)
                                        .FirstOrDefaultAsync(ev => ev.EventoId == id);
            
            return eventoRetornado;           
        }

        public async Task<Evento> Post(Evento evento)
        {
            await context.Evento.AddAsync (evento);
            await context.SaveChangesAsync ();
            return evento;
        }

        public async Task<Evento> Put(Evento evento)
        {
           context.Entry (evento).State = EntityState.Modified;
            await context.SaveChangesAsync ();
            return evento;
        }
    }
}