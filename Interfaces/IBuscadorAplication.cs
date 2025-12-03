using Buscador.Dtos;
using Buscador.Models;

namespace Buscador.Interfaces
{
    public interface IBuscadorAplication
    {
        Task<List<SituacaoDto>> BuscarSituacoesAsync(string pesquisa);

        Task<List<SituacaoDto>> CriaSituacoesAsync(CriarSituacaoDto add);
    }
}
