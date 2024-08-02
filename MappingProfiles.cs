using AutoMapper;
using PublishingHouse.Models;
using PublishingHouse.Models.Dtos.Author;
using PublishingHouse.Models.Dtos.City;
using PublishingHouse.Models.Dtos.Country;
using PublishingHouse.Models.Dtos.Gender;
using PublishingHouse.Models.Dtos.Product;
using PublishingHouse.Models.Dtos.ProductAuthor;
using PublishingHouse.Models.Dtos.ProductType;
using PublishingHouse.Models.Dtos.Publisher;

namespace PublishingHouse;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		// Mapping for AuthorDtos
		CreateMap<Author, AuthorDto>().ReverseMap();
		CreateMap<CreateAuthorDto, Author>();
		CreateMap<UpdateAuthorDto, Author>();

		// Mapping for ProductDtos
		CreateMap<Product, ProductDto>().ReverseMap();
		CreateMap<CreateProductDto, Product>();
		CreateMap<UpdateProductDto, Product>();

		// Mapping for GenderDtos
		CreateMap<Gender, GenderDto>().ReverseMap();
		CreateMap<CreateGenderDto, Gender>();
		CreateMap<UpdateGenderDto, Gender>();

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
		CreateMap<UpdateProductTypeDto, ProductType>();

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
