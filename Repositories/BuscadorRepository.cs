using Buscador.Dtos;
using Buscador.Infrastructure.Data;
using Buscador.Interfaces;
using Buscador.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
                .Where(s => s.Ativo == true)
                .Select(s => new SituacaoDto
            {
                Id = s.Id,
                ProblemaDescricao = s.ProblemaDescricao,
                SolucaoDescricao = s.SolucaoDescricao,
                DataRegistro = s.DataRegistro
            }).ToListAsync();

            return resultado;
        }

        public async Task<SituacaoDto?> BuscarProblemaDescricaoAsync(string pesquisa)
        {
            IQueryable<Situacao> query = _context.Situacoes;

            query = query.Where(x => x.ProblemaDescricao == pesquisa);

            var resultado = await query
               .Select(s => new SituacaoDto
               {
                   Id = s.Id,
                   ProblemaDescricao = s.ProblemaDescricao,
                   SolucaoDescricao = s.SolucaoDescricao,
                   DataRegistro = s.DataRegistro,
               }).FirstOrDefaultAsync();

            return resultado;
        }

        public async Task<bool> ExisteProblemaDescricaoAsync(string pesquisa)
        {
            var data = await _context.Situacoes
                .AnyAsync(s => s.ProblemaDescricao == pesquisa);

            return data;
        }

        public async Task<bool> GetIdAsync(int id)
        {
            var data = await _context.Situacoes
                .AnyAsync(s => s.Id == id);

            return data;
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _context.Situacoes
                .FirstOrDefaultAsync(s => s.Id == id);

            if (data != null)
            {
                data.DesativaRegistro();
                data.AtualizaDataRegistro();
                await _context.SaveChangesAsync();
            }
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
