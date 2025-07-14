using Mouts.SalesDeveloper.Application.Dtos;
using AutoMapper;
using Mouts.SalesDeveloper.Domain.Entities;

namespace Mouts.SalesDeveloper.Application.Sales.Mappings
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SaleRequest, Sale>()
                .ForMember(dest => dest.Status, opt => opt.Ignore());

            CreateMap<SaleItemRequest, SaleItem>();

            CreateMap<SaleItem, SaleItemResponse>();

            CreateMap<Sale, SaleResponse>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items ?? new List<SaleItem>()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
