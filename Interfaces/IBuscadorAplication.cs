using Buscador.Dtos;
using Buscador.Dtos.Requests;
using Buscador.Dtos.Responses;

namespace Buscador.Interfaces
{
    public interface IBuscadorAplication
    {
        Task<SituacaoResponse> BuscarSituacoesAsync(int id);

        Task<SituacaoResponse> CriaSituacoesAsync(CriarSituacaoRequest request);

        Task<List<SituacaoResponse>> ListAllAsync(string pesquisa);

        Task<bool> DeleteSituacoesAsync(int id);
    }
}
