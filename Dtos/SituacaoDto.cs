using Buscador.Models;

namespace Buscador.Dtos
{
    public class SituacaoDto
    {
        public int Id { get; set; }
        public string ProblemaDescricao { get; set; }
        public string SolucaoDescricao { get; set; }
        public DateTime DataRegistro { get; set; }

        public static implicit operator SituacaoDto?(Situacao? v)
        {
            throw new NotImplementedException();
        }
    }
}
