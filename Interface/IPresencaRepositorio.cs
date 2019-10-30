using System.Collections.Generic;
using System.Threading.Tasks;
using Gufos.Models;

namespace Gufos.Interface
{
    public interface IPresencaRepositorio
    {
        Task<List<Presenca>> Get();
        Task<Presenca> Get(int id);

        Task<Presenca> Post(Presenca presenca);

        Task<Presenca> Put(Presenca presenca);

        Task<Presenca> Delete(Presenca presencaRetornada);   
    }
}