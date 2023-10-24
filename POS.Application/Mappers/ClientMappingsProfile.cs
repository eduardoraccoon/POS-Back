using AutoMapper;
using POS.Application.Dtos.Client.Request;
using POS.Application.Dtos.Client.Response;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappers
{
    public class ClientMappingsProfile : Profile
    {
        public ClientMappingsProfile()
        {
            CreateMap<Client, ClientResponseDto>()
               .ForMember(x => x.ClientId, x => x.MapFrom(y => y.Id))
               .ReverseMap();

            CreateMap<ClientRequestDto, Client>();
        }
    }
}
