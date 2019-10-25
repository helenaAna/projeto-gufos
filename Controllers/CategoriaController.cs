using System.Collections.Generic;
using System.Threading.Tasks;
using Gufos.Models;
using Gufos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gufos.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CategoriaController : ControllerBase {
        
        CategoriaRepositorio repositorio = new CategoriaRepositorio();

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get()
        {
            List<Categoria> listaDeCategoria = await repositorio.Get();
            
            if(listaDeCategoria == null)
            {
                return NotFound();
            }
            return listaDeCategoria;
        }
        [HttpGet("{id}")]
         public async Task<ActionResult<Categoria>> Get(int id)
         {
            Categoria categoriaRetornada = await repositorio.Get(id);
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
                await repositorio.Post(categoria);
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
            
            try
            {
                await repositorio.Put(categoria);
            }
            catch(DbUpdateConcurrencyException)
            {
                var categoriaValida = await repositorio.Get(id);
                if (categoriaValida == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return categoria;
        }
        [HttpDelete("{id}")]

        public async Task<ActionResult<Categoria>> Delete (int id)
        {
        Categoria categoriaRetornada = await repositorio.Get(id);
        if (categoriaRetornada == null)
        {
            return NotFound();
        }
        await repositorio.Delete(categoriaRetornada);    
        return categoriaRetornada;
        }
    }
}