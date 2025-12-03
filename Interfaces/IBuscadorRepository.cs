using Buscador.Dtos;
using Buscador.Models;

namespace Buscador.Interfaces
{
    public interface IBuscadorRepository
    {
        Task<List<SituacaoDto>> BuscarSituacoesRepAsync(string pesquisa);

        Task<List<SituacaoDto>> BuscarProblemaDescricaoAsync(string pesquisa);

        Task AddSituacaoAsync(Situacao situacao);

        Task<int> SaveChangesAsync();
    }
}
