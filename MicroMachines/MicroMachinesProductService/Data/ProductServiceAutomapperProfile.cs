using AutoMapper;
using MicroMachinesCommon.Dtos;
using MicroMachinesProductService.Models;

namespace MicroMachinesProductService.Data;

public class ProductServiceAutomapperProfile : Profile
{
    public ProductServiceAutomapperProfile()
    {
        CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductUpdateDto, Product>();
        CreateMap<Product, ProductReadDto>();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>();
        CreateMap<Category, CategoryReadDto>();
    }
}
