using System.Collections.Generic;
using System.Threading.Tasks;
using Gufos.Models;
using Gufos.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gufos.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    [Produces ("application/json")]
    public class EventoController : ControllerBase {
        EventoRepositorio repositorio = new EventoRepositorio();
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get() {
            List<Evento> listaDeEvento = await repositorio.Get();
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
        public async Task<ActionResult<Evento>> Get (int id) 
        {
            Evento eventoRetornado = await repositorio.Get(id);
            if (eventoRetornado == null) {
                return NotFound ();
            }
            return eventoRetornado;
        }

        [HttpPost]
        public async Task<ActionResult<Evento>> Post (Evento evento) 
        {
            try {
                await repositorio.Post(evento);

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
            try {
                await repositorio.Put(evento);
            } catch (DbUpdateConcurrencyException) 
            {
                var eventoValido = await repositorio.Get(id);
                if (eventoValido == null) 
                {
                    return NotFound ();
                }
                else
                {
                    throw;
                }
            }
            return evento;
        }

        [HttpDelete ("{id}")]

        public async Task<ActionResult<Evento>> Delete (int id)
        {
            Evento eventoRetornado = await repositorio.Get(id);    
            if (eventoRetornado == null) {
                return NotFound ();
            }
            await repositorio.Delete(eventoRetornado);
            return eventoRetornado;
        }

    }
}