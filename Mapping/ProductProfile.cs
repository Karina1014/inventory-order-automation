using AutoMapper;
using InventoryFlow.Models;
using InventoryFlow.Models.Dtos;

namespace InventoryFlow.Mapping;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
            .ForMember(dest => dest.TotalStock, opt => opt.MapFrom(src => src.ProductInventories != null ? src.ProductInventories.Sum(i => i.CurrentStock) : 0));
        CreateMap<ProductDto, Product>();

        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, UpdateProductDto>().ReverseMap();
    }
}
