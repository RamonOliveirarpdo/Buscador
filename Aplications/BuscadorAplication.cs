using Buscador.Dtos;
using Buscador.Interfaces;
using Buscador.Models;
using Microsoft.EntityFrameworkCore;
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

            var situacaoList = await _buscadorRepository.BuscarSituacoesAsync(pesquisa);
            var SituacaoDtoList = ReturnSituacaoDto(situacaoList);

            return SituacaoDtoList;
        }

        public async Task<SituacaoDto> CriaSituacoesAsync(CriarSituacaoDto add)
        {

            var existingSituacoes = await _buscadorRepository.ExisteProblemaDescricaoAsync(add.ProblemaDescricao);

            if (existingSituacoes != null)
            {
                throw new InvalidOperationException("A situação já está cadastrada no sistema. Não é possível duplicar.");
            }

            var situacao = new Situacao
            {
                ProblemaDescricao = add.ProblemaDescricao,
                SolucaoDescricao = add.SolucaoDescricao,
                DataRegistro = DateTime.UtcNow,
                Ativo = true,
                DataAtualizacao = DateTime.UtcNow
            };

             var situacaoSalva = await _buscadorRepository.AddSituacaoAsync(situacao);
             var situacaoDto = ReturnSituacaoDto(situacaoSalva);
            await _buscadorRepository.SaveChangesAsync();

            return situacaoDto;
        }

        public async Task<bool> DeleteSituacoesAsync(int id)
        {

            if (!await _buscadorRepository.GetIdAsync(id))
            {
                return false;
            }

            await _buscadorRepository.DeleteAsync(id);
            await _buscadorRepository.SaveChangesAsync();

            return true;
        }

        private SituacaoDto ReturnSituacaoDto(Situacao situacao)
        {

            return new SituacaoDto
            {
                Id = situacao.Id,
                ProblemaDescricao = situacao.ProblemaDescricao,
                SolucaoDescricao = situacao.SolucaoDescricao,
                DataRegistro = situacao.DataRegistro
            };
        }

        private List<SituacaoDto> ReturnSituacaoDto(List<Situacao> situacaoList)
        {

            return situacaoList.Select(situacao => new SituacaoDto
            {
                Id = situacao.Id,
                ProblemaDescricao = situacao.ProblemaDescricao,
                SolucaoDescricao = situacao.SolucaoDescricao,
                DataRegistro = situacao.DataRegistro
            }).ToList();
        }
    }
}
