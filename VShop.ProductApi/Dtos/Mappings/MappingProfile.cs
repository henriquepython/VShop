﻿using AutoMapper;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Dtos.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();

        CreateMap<ProductDto, Product>()

        CreateMap<Product, ProductDto>()
            .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
    }
}
