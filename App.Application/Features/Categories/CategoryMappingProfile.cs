using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;
using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Categories
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CreateCategoryRequest, Category>().ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
            CreateMap<UpdateCategoryRequest, Category>().ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
            CreateMap<Category, CategoryWithProductsDto>();
        }
    }
}
