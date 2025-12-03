using Buscador.Dtos;
using Buscador.Infrastructure.Data;
using Buscador.Interfaces;
using Buscador.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Buscador.Repositories
{
    public class BuscadorRepository : IBuscadorRepository
    {
        private readonly ApplicationDbContext _context;

        public BuscadorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SituacaoDto>> BuscarSituacoesRepAsync(string pesquisa)
        {
            IQueryable<Situacao> query = _context.Situacoes;

            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                query = _context.Situacoes
                    .Where(s => s.ProblemaDescricao.Contains(pesquisa));
            }

            var resultado = await query
                .Select(s => new SituacaoDto
            {
                Id = s.Id,
                ProblemaDescricao = s.ProblemaDescricao,
                SolucaoDescricao = s.SolucaoDescricao,
                DataRegistro = s.DataRegistro
            }).ToListAsync();

            return resultado;
        }

        public async Task<List<SituacaoDto>> BuscarProblemaDescricaoAsync(string pesquisa)
        {
            var resultado = _context.Situacoes
                .Where(s => s.ProblemaDescricao == pesquisa)
                .Select(s => new SituacaoDto
                {
                    Id = s.Id,
                    ProblemaDescricao = s.ProblemaDescricao,
                    SolucaoDescricao = s.SolucaoDescricao,
                    DataRegistro = s.DataRegistro
                });

            return await resultado.ToListAsync();
        }

        public async Task AddSituacaoAsync(Situacao situacao)
        {
            await _context.Situacoes.AddAsync(situacao);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
