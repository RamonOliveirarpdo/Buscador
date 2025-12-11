using Buscador.Dtos;
using Buscador.Models;
using System.Net;

namespace Buscador.Interfaces
{
    public interface IBuscadorAplication
    {
        Task<List<SituacaoDto>> BuscarSituacoesAsync(string pesquisa);

        Task<SituacaoDto> CriaSituacoesAsync(CriarSituacaoDto add);

        Task<HttpStatusCode> DeleteSituacoesAsync(int id);
    }
}
