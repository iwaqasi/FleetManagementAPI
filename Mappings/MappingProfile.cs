// FleetManagementAPI/Mappings/MappingProfile.cs
using AutoMapper;
using FleetManagementAPI.Controllers;
using FleetManagementAPI.DTOs;
using FleetManagementAPI.Models;

namespace FleetManagementAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.SubCategories));

            CreateMap<CategoryRequest, Category>();

            CreateMap<Employee, EmployeeCreationDto>().ReverseMap();
        }
    }
}