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

        public async Task<Situacao?> GetIdAsync(int id)
        {
            return await _context.Situacoes
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Situacao>> BuscarSituacoesAsync(string pesquisa)
        {
            var query = _context.Situacoes.AsNoTracking().Where(s => s.Ativo);

            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                query = query
                    .Where(s => s.ProblemaDescricao.Contains(pesquisa));
            }

            return await query.ToListAsync();
        }

        public async Task<Situacao?> BuscarProblemaDescricaoAsync(string pesquisa)
        {
            return await _context.Situacoes
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.ProblemaDescricao == pesquisa);
        }

        public async Task<Situacao?> ExisteProblemaDescricaoAsync(string pesquisa)
        {
            return await _context.Situacoes
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.ProblemaDescricao == pesquisa);
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

        public Task<bool> ExistsAsync(int id)
        {
            IQueryable<Situacao> query = _context.Situacoes;

            return query
                .AnyAsync(s => s.Id == id);
        }
    }
}
