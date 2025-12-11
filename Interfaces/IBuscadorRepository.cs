using Buscador.Dtos;
using Buscador.Models;

namespace Buscador.Interfaces
{
    public interface IBuscadorRepository
    {
        Task<List<SituacaoDto>> BuscarSituacoesRepAsync(string pesquisa);

        Task<SituacaoDto> BuscarProblemaDescricaoAsync(string pesquisa);

        Task<bool> GetIdAsync(int id);

        Task DeleteAsync(int id);

        Task<bool> ExisteProblemaDescricaoAsync(string pesquisa);

        Task AddSituacaoAsync(Situacao situacao);

        Task<int> SaveChangesAsync();
    }
}
