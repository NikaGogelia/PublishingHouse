using AutoMapper;
using PublishingHouse.Data.Entities;
using PublishingHouse.Models;
using PublishingHouse.Models.Dtos.Author;
using PublishingHouse.Models.Dtos.City;
using PublishingHouse.Models.Dtos.Country;
using PublishingHouse.Models.Dtos.Gender;
using PublishingHouse.Models.Dtos.Product;
using PublishingHouse.Models.Dtos.ProductAuthor;
using PublishingHouse.Models.Dtos.ProductType;
using PublishingHouse.Models.Dtos.Publisher;
using PublishingHouse.Shared.Dtos.Auth;

namespace PublishingHouse;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		// User Mapping
		CreateMap<RegisterDto, User>()
			.ForMember(dest => dest.Password, opt => opt.Ignore());
		CreateMap<User, RegisterDto>();
		CreateMap<User, LoginDto>();
		CreateMap<LoginDto, User>();
		CreateMap<User, AuthResponseDto>();

		// Mapping for AuthorDtos
		CreateMap<Author, AuthorDto>().ReverseMap();
		CreateMap<Author, AuthorByIdDto>()
			.ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
			.ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
			.ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
			.ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.ProductAuthors.Select(pa => pa.Product)));
		CreateMap<CreateAuthorDto, Author>();
		CreateMap<UpdateAuthorDto, Author>();

		// Mapping for ProductDtos
		CreateMap<Product, ProductDto>().ReverseMap();
		CreateMap<Product, ProductByIdDto>()
			.ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
			.ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType))
			.ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.ProductAuthors.Select(pa => pa.Author)));
		CreateMap<CreateProductDto, Product>();
		CreateMap<UpdateProductDto, Product>();

		// Mapping for GenderDtos
		CreateMap<Gender, GenderDto>().ReverseMap();
		CreateMap<CreateGenderDto, Gender>();

		// Mapping for CityDtos
		CreateMap<City, CityDto>().ReverseMap();
		CreateMap<CreateCityDto, City>();
		CreateMap<UpdateCityDto, City>();

		// Mapping for CountryDtos
		CreateMap<Country, CountryDto>().ReverseMap();
		CreateMap<CreateCountryDto, Country>();
		CreateMap<UpdateCountryDto, Country>();

		// Mapping for ProductTypeDtos
		CreateMap<ProductType, ProductTypeDto>().ReverseMap();
		CreateMap<CreateProductTypeDto, ProductType>();

		// Mapping for PublisherDtos
		CreateMap<Publisher, PublisherDto>().ReverseMap();
		CreateMap<CreatePublisherDto, Publisher>();
		CreateMap<UpdatePublisherDto, Publisher>();

		// Mapping for ProductAuthorDtos
		CreateMap<ProductAuthor, ProductAuthorDto>().ReverseMap();
		CreateMap<CreateProductAuthorDto, ProductAuthor>();
		CreateMap<UpdateProductAuthorDto, ProductAuthor>();
	}
}
