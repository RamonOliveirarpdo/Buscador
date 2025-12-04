namespace Buscador.Dtos.Responses
{
    public class SituacaoResponse
    {
        public int Id { get; set; }
        public string? ProblemaDescricao { get; set; }
        public string? SolucaoDescricao { get; set; }
        public DateTime DataRegistro { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
