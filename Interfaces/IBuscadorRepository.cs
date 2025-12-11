using Buscador.Dtos;
using Buscador.Models;

namespace Buscador.Interfaces
{
    public interface IBuscadorRepository
    {
        Task<List<SituacaoDto>> BuscarSituacoesRepAsync(string pesquisa);

        Task<SituacaoDto> BuscarProblemaDescricaoAsync(string pesquisa);

        Task<bool> ExisteProblemaDescricaoAsync(string pesquisa);

        Task AddSituacaoAsync(Situacao situacao);

        Task<int> SaveChangesAsync();
    }
}
