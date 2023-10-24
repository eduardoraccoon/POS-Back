using AutoMapper;
using POS.Application.Dtos.Provider.Request;
using POS.Application.Dtos.Provider.Response;
using POS.Domain.Entities;

namespace POS.Application.Mappers
{
    public class ProviderMappingsProfile : Profile
    {
        public ProviderMappingsProfile()
        {
            CreateMap<Provider, ProviderResponseDto>()
              .ForMember(x => x.ProviderId, x => x.MapFrom(y => y.Id))
              .ReverseMap();

            CreateMap<ProviderRequestDto, Provider>();
        }
    }
}
