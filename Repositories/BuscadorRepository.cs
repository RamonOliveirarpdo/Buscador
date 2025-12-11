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

        public async Task<List<Situacao>> BuscarSituacoesAsync(string pesquisa)
        {
            IQueryable<Situacao> query = _context.Situacoes;

            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                query = query
                    .Where(s => s.ProblemaDescricao.Contains(pesquisa));
            }

            var resultado = await query
                .Where(s => s.Ativo == true)
                .Select(s => new Situacao
            {
                Id = s.Id,
                ProblemaDescricao = s.ProblemaDescricao,
                SolucaoDescricao = s.SolucaoDescricao,
                DataRegistro = s.DataRegistro
            }).ToListAsync();

            return resultado;
        }

        public async Task<Situacao> BuscarProblemaDescricaoAsync(string pesquisa)
        {
            IQueryable<Situacao> query = _context.Situacoes;

            query = query.Where(x => x.ProblemaDescricao == pesquisa);

            var resultado = await query
               .Select(s => new Situacao
               {
                   Id = s.Id,
                   ProblemaDescricao = s.ProblemaDescricao,
                   SolucaoDescricao = s.SolucaoDescricao,
                   DataRegistro = s.DataRegistro,
               }).FirstOrDefaultAsync();

            return resultado;
        }

        public async Task<Situacao> ExisteProblemaDescricaoAsync(string pesquisa)
        {
            var query =  _context.Situacoes
                .Where(s => s.ProblemaDescricao == pesquisa);

            var resultado = await query
                .Select(s => new Situacao
                {
                    Id = s.Id,
                    ProblemaDescricao = s.ProblemaDescricao,
                    SolucaoDescricao = s.SolucaoDescricao,
                    DataRegistro = s.DataRegistro,
                }).FirstOrDefaultAsync();

            return resultado;
        }

        public async Task<bool> GetIdAsync(int id)
        {
            var data = await _context.Situacoes
                .AnyAsync(s => s.Id == id);

            return data;
        }

        public async Task DeleteAsync(int id)
        {
            var query = await _context.Situacoes
                .FirstOrDefaultAsync(s => s.Id == id);

            if (query != null)
            {
                query.DesativaRegistro();
                query.AtualizaDataRegistro();
            }
        }

        public async Task<Situacao> AddSituacaoAsync(Situacao situacao)
        {
            await _context.Situacoes.AddAsync(situacao);

            return situacao;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
