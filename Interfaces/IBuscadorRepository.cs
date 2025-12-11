using Buscador.Dtos;
using Buscador.Models;

namespace Buscador.Interfaces
{
    public interface IBuscadorRepository
    {
        Task<List<Situacao>> BuscarSituacoesAsync(string pesquisa);

        Task<Situacao> BuscarProblemaDescricaoAsync(string pesquisa);

        Task<bool> GetIdAsync(int id);

        Task DeleteAsync(int id);

        Task<Situacao> ExisteProblemaDescricaoAsync(string pesquisa);

        Task<Situacao> AddSituacaoAsync(Situacao situacao);

        Task<int> SaveChangesAsync();
    }
}
