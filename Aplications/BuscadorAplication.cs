using Buscador.Dtos;
using Buscador.Interfaces;
using Buscador.Models;

namespace Buscador.Aplications
{
    public class BuscadorAplication : IBuscadorAplication
    {
        private readonly IBuscadorRepository _buscadorRepository;

        public BuscadorAplication(IBuscadorRepository buscadorRepository)
        {
            _buscadorRepository = buscadorRepository;
        }

        public async Task<List<SituacaoDto>> BuscarSituacoesAsync(string pesquisa)
        {
            var busca = await _buscadorRepository.BuscarSituacoesRepAsync(pesquisa);
            return busca;
        }

        public async Task<List<SituacaoDto>> CriaSituacoesAsync(CriarSituacaoDto add)
        {
            var existingSituacoes = await _buscadorRepository.BuscarProblemaDescricaoAsync(add.ProblemaDescricao);
            if (existingSituacoes.Count() > 0)
            {
                return existingSituacoes;  
            }

            var situacao = new Situacao
            {
                ProblemaDescricao = add.ProblemaDescricao,
                SolucaoDescricao = add.SolucaoDescricao,
                DataRegistro = DateTime.UtcNow
            };

            await _buscadorRepository.AddSituacaoAsync(situacao);
             await _buscadorRepository.SaveChangesAsync();

            var response = await _buscadorRepository.BuscarProblemaDescricaoAsync(situacao.ProblemaDescricao);

            return response;
        }
    }
}
