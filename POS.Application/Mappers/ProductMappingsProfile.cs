using AutoMapper;
using POS.Application.Dtos.Product.Request;
using POS.Application.Dtos.Product.Response;
using POS.Domain.Entities;

namespace POS.Application.Mappers
{
    public class ProductMappingsProfile : Profile
    {
        public ProductMappingsProfile()
        {
            CreateMap<Product, ProductResponseDto>()
              .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Id))
              .ReverseMap();

            CreateMap<ProductRequestDto, Product>();
        }
    }
}
