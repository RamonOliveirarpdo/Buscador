using Microsoft.EntityFrameworkCore;

namespace Buscador.Models
{
    public class Situacao : DbContext
    {
        public int Id { get; set; }
        public string? ProblemaDescricao { get; set; }
        public string? SolucaoDescricao { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
