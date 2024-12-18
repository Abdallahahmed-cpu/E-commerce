using AutoMapper;
using E_commers.Application.DTOS.CategoryDTO;
using E_commers.Application.DTOS.ProductDTO;
using E_commers.Domain.Models;

namespace E_commers.Application.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CreateCategory, Category>();
            CreateMap<CreateProduct, Product>();



            CreateMap<Product, GetProduct>();
            CreateMap<Category, GetCategory>();

            CreateMap<UpdateCategory, Category>();
            CreateMap<UpdateProduct, Product>();
        }
    }
}
