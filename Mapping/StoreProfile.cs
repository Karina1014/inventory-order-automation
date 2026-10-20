using AutoMapper;
using InventoryFlow.Models;
using InventoryFlow.Models.Dtos;

namespace InventoryFlow.Mapping;

public class StoreProfile : Profile
{
    public StoreProfile()
    {
    
        CreateMap<Store, StoreDto>().ReverseMap();
        CreateMap<Store, CreateStoreDto>().ReverseMap();
        CreateMap<Store, UpdateStoreDto>().ReverseMap();
    }
}
