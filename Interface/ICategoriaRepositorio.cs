using System.Collections.Generic;
using System.Threading.Tasks;
using Gufos.Models;

namespace Gufos.Interface
{
    public interface ICategoriaRepositorio
    {
        Task<List<Categoria>> Get();
        Task<Categoria> Get(int id);

        Task<Categoria> Post(Categoria categoria);

        Task<Categoria> Put(Categoria categoria);

        Task<Categoria> Delete(Categoria categoriaRetornada);       
    }
}