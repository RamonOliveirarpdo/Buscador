using Buscador.Models;
using Microsoft.EntityFrameworkCore;

namespace Buscador.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Situacao> Situacoes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Situacao>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<User>()
                .HasKey(s => s.Id);
        }
    }
}
