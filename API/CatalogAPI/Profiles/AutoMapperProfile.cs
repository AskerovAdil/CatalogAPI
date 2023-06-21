using AutoMapper;
using CatalogAPI.Models;

namespace CatalogAPI.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductDto, Product>();
        }
    }
}
