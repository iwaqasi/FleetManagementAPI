using AutoMapper;
using FleetManagementAPI.DTOs;
using FleetManagementAPI.Models;

namespace FleetManagementAPI.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<Product, ProductListDto>()
                //.ForMember(d=>d.ProductCategoryName, o=>o.MapFrom(s=>s.ProductCategory.Description))
                //.ForMember(d=>d.ProductSupplierName, o=>o.MapFrom(s=>s.ProductSupplier.CompanyName))
                .ReverseMap();

        }
    }
}
