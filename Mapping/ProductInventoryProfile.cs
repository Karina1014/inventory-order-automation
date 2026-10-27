using AutoMapper;
using InventoryFlow.Models;
using InventoryFlow.Models.Dtos;

namespace InventoryFlow.Mapping;

public class ProductInventoryProfile : Profile
{
    public ProductInventoryProfile()
    {
        CreateMap<ProductInventory, ProductInventoryDto>()
            .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store != null ? src.Store.Name : string.Empty))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty))
            .ForMember(dest => dest.ProductSku, opt => opt.MapFrom(src => src.Product != null ? src.Product.Sku : string.Empty))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Product != null && src.Product.Category != null ? src.Product.Category.Name : string.Empty))
            .ForMember(dest => dest.MinimumStockThreshold, opt => opt.MapFrom(src => src.Product != null ? src.Product.MinimumStockThreshold : 0))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product != null ? src.Product.UnitPrice : 0m));

        CreateMap<ProductInventoryDto, ProductInventory>();
    }
}
