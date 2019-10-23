using System.Collections.Generic;
using System.Threading.Tasks;
using Gufos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gufos.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CategoriaController : ControllerBase {
        GufosContext context = new GufosContext();

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get()
        {
            List<Categoria> listaDeCategoria = await context.Categoria.ToListAsync();
            
            if(listaDeCategoria == null)
            {
                return NotFound();
            }
            return listaDeCategoria;
        }
        [HttpGet("{id}")]
         public async Task<ActionResult<Categoria>> Get(int id)
         {
            Categoria categoriaRetornada = await context.Categoria.FindAsync(id);
            if(categoriaRetornada == null)
            {
                return NotFound();
            }
            return categoriaRetornada;
         }
         [HttpPost]
         public async Task<ActionResult<Categoria>> Post(Categoria categoria)
         {
             try
             {
                await context.Categoria.AddAsync(categoria);
                await context.SaveChangesAsync(); 

             }
             
             catch (System.Exception)
             {
                 
                throw;
             }
             return categoria;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Categoria>> Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }
            context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                var categoriaValida = context.Categoria.FindAsync(id);
                if (categoriaValida == null)
                {
                    return NotFound();
                }
            }

            await context.SaveChangesAsync();

            return categoria;
        }
        [HttpDelete("{id}")]

        public async Task<ActionResult<Categoria>> Delete (int id){
        Categoria categoriaRetornada = await context.Categoria.FindAsync(id);
        if (categoriaRetornada == null)
        {
            return NotFound();
        }
        context.Categoria.Remove(categoriaRetornada);
        await context.SaveChangesAsync();

        return categoriaRetornada;
        }
    }
}