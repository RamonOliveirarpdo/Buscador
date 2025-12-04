using AutoMapper;
using Buscador.Dtos;
using Buscador.Dtos.Requests;
using Buscador.Dtos.Responses;
using Buscador.Interfaces;
using Buscador.Models;

namespace Buscador.Aplications
{
    public class BuscadorAplication : IBuscadorAplication
    {
        private readonly IBuscadorRepository _buscadorRepository;
        private readonly IMapper _mapper;

        public BuscadorAplication(IBuscadorRepository buscadorRepository, IMapper mapper)
        {
            _buscadorRepository = buscadorRepository;
            _mapper = mapper;
        }

        public async Task<SituacaoResponse> BuscarSituacoesAsync(int id)
        {
            var situacao = await _buscadorRepository.GetIdAsync(id);
            if(situacao == null)
            {
                throw new KeyNotFoundException("A situação não foi encontrada.");
            }

            return _mapper.Map<SituacaoResponse>(situacao);
        }

        public async Task<List<SituacaoResponse>> ListAllAsync(string pesquisa)
        {
            var situacaoList = await _buscadorRepository.BuscarSituacoesAsync(pesquisa);
            if (situacaoList == null || !situacaoList.Any())
            {
                throw new KeyNotFoundException("O registro solicitado não foi encontrado.");
            }

            return _mapper.Map<List<SituacaoResponse>>(situacaoList);
        }

        public async Task<SituacaoResponse> CriaSituacoesAsync(CriarSituacaoRequest request)
        {
            var existingSituacoes = await _buscadorRepository.ExisteProblemaDescricaoAsync(request.ProblemaDescricao);

            if (existingSituacoes != null)
            {
                throw new InvalidOperationException("A situação já está cadastrada no sistema. Não é possível duplicar.");
            }

            var situacao = _mapper.Map<Situacao>(request);

            situacao = await _buscadorRepository.AddSituacaoAsync(situacao);
            await _buscadorRepository.SaveChangesAsync();

            return _mapper.Map<SituacaoResponse>(situacao);
        }

        public async Task<bool> DeleteSituacoesAsync(int id)
        {

            if (!await _buscadorRepository.ExistsAsync(id))
            {
                 throw new KeyNotFoundException("O registro solicitado não foi encontrado.");
            }

            await _buscadorRepository.DeleteAsync(id);
            await _buscadorRepository.SaveChangesAsync();

            return true;
        }
    }
}
