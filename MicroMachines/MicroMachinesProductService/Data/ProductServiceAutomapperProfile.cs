using AutoMapper;
using MicroMachinesCommon.Dtos;
using MicroMachinesProductService.Models;

namespace MicroMachinesProductService.Data;

public class ProductServiceAutomapperProfile : Profile
{
    public ProductServiceAutomapperProfile()
    {
        CreateMap<ProductCreateDto, Product>();
        CreateMap<Product, ProductReadDto>();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<Category, CategoryReadDto>();
    }
}
