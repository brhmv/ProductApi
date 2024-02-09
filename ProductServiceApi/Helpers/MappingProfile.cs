using AutoMapper;
using ProductServiceApi.Models.ViewModels;
using ProductServiceApi.Models.Entities;

namespace ProductServiceApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductViewModel, Product>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                //.ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath)).ReverseMap();

            CreateMap<CategoryViewModel, Category>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap();


        }
    }
}