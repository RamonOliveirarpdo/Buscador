namespace Buscador.Models
{
    public class Situacao
    {
        public int Id { get; set; }
        public string? ProblemaDescricao { get; set; }
        public string? SolucaoDescricao { get; set; }
        public DateTime DataRegistro { get; set; } = DateTime.UtcNow;
        public bool Ativo { get; set; } = true;
        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

        public bool DesativaRegistro()
        {
            Ativo = false;

            return Ativo;
        }

        public DateTime AtualizaDataRegistro()
        {
            DataAtualizacao = DateTime.UtcNow;

            return DataAtualizacao;
        }
    }
}
