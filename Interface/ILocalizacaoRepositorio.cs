using System.Collections.Generic;
using System.Threading.Tasks;
using Gufos.Models;

namespace Gufos.Interface
{
    public interface ILocalizacaoRepositorio
    {
        Task<List<Localizacao>> Get();
        Task<Localizacao> Get(int id);

        Task<Localizacao> Post(Localizacao localizacao);

        Task<Localizacao> Put(Localizacao localizacao);

        Task<Localizacao> Delete(Localizacao localizacaoRetornada);  
    }
}