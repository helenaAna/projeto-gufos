using System.Collections.Generic;
using System.Threading.Tasks;
using Gufos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gufos.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    [Produces ("application/json")]
    public class EventoController : ControllerBase {
        GufosContext context = new GufosContext ();
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get () {
            List<Evento> listaDeEvento = await context.Evento.Include (c => c.Categoria).Include(l => l.Localizacao).ToListAsync ();

            if (listaDeEvento == null) {
                return NotFound ();
            }
            foreach(var item in listaDeEvento)
            {
                item.Categoria.Evento = null;
                item.Localizacao.Evento = null;
            }
            return listaDeEvento;
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<Evento>> Get (int id) {
            Evento eventoRetornado = await context.Evento.FindAsync (id);
            if (eventoRetornado == null) {
                return NotFound ();
            }
            return eventoRetornado;
        }

        [HttpPost]
        public async Task<ActionResult<Evento>> Post (Evento evento) {
            try {
                await context.Evento.AddAsync (evento);
                await context.SaveChangesAsync ();

            } catch (System.Exception) {

                throw;
            }
            return evento;
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult<Evento>> Put (int id, Evento evento) {
            if (id != evento.EventoId) {
                return BadRequest ();
            }
            context.Entry (evento).State = EntityState.Modified;

            try {
                await context.SaveChangesAsync ();
            } catch (DbUpdateConcurrencyException) {
                var eventoValido = context.Evento.FindAsync (id);
                if (eventoValido == null) {
                    return NotFound ();
                }
            }

            await context.SaveChangesAsync ();

            return evento;
        }

        [HttpDelete ("{id}")]

        public async Task<ActionResult<Evento>> Delete (int id) {
            Evento eventoRetornado = await context.Evento.Include(c => c.Categoria).Include(l=> l.Localizacao).FirstOrDefaultAsync(e=>e.EventoId == id);
            if (eventoRetornado == null) {
                return NotFound ();
            }
            context.Evento.Remove (eventoRetornado);
            await context.SaveChangesAsync ();

            return eventoRetornado;
        }

    }
}