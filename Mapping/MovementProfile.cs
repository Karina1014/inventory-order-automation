using AutoMapper;
using InventoryFlow.Models;
using InventoryFlow.Models.Dtos;

namespace InventoryFlow.Mapping;

public class MovementProfile : Profile
{
    public MovementProfile()
    {
        CreateMap<MovementType, MovementTypeDto>().ReverseMap();

        CreateMap<InventoryMovement, InventoryMovementDto>()
            .ForMember(dest => dest.MovementTypeName, opt => opt.MapFrom(src => src.MovementType.Name))
            .ForMember(dest => dest.IsPositive, opt => opt.MapFrom(src => src.MovementType.IsPositive))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ProductSku, opt => opt.MapFrom(src => src.Product.Sku))
            .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.Name));

        CreateMap<InventoryMovementDto, InventoryMovement>();
        CreateMap<CreateMovementDto, InventoryMovement>();
    }
}
