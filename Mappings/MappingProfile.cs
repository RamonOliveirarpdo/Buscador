using AutoMapper;
using Buscador.Dtos.Requests;
using Buscador.Dtos.Responses;
using Buscador.Models;

namespace Buscador.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Request -> Entity
            CreateMap<CriarSituacaoRequest, Situacao>();

            // Entity -> Response
            CreateMap<Situacao, SituacaoResponse>();
        }
    }
}
