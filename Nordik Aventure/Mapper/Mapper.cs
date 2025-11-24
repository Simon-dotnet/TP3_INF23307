using AutoMapper;
using GestBibli.Objects.ViewModels;
using Nordik_Aventure.Objects.Models.Finance;

namespace Nordik_Aventure.Mapper;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<OrderItemCreateViewModel, PurchaseDetails>()
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Purchase, opt => opt.Ignore())
            .ForMember(dest => dest.PurchaseId, opt => opt.Ignore());
    }
}