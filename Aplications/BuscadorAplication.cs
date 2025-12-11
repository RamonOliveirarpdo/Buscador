using Buscador.Dtos;
using Buscador.Interfaces;
using Buscador.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        public async Task<SituacaoDto> CriaSituacoesAsync(CriarSituacaoDto add)
        {
            bool existingSituacoes = await _buscadorRepository.ExisteProblemaDescricaoAsync(add.ProblemaDescricao);
            if (existingSituacoes == true)
            {
                var responseExiste = await _buscadorRepository.BuscarProblemaDescricaoAsync(add.ProblemaDescricao);

                return responseExiste;  
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

        public async Task<HttpStatusCode> DeleteSituacoesAsync(int id)
        {
            bool existingSituacoes = await _buscadorRepository.GetIdAsync(id);
            if (existingSituacoes == false)
            {

                return HttpStatusCode.NotFound;
            }

            await _buscadorRepository.DeleteAsync(id);

            return HttpStatusCode.OK;
        }
    }
}
