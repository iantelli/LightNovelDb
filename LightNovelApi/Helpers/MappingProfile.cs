using AutoMapper;
using LightNovelApi.Dto;
using LightNovelApi.Models;

namespace LightNovelApi.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Novel, NovelDto>().ReverseMap();
        CreateMap<Genre, GenreDto>().ReverseMap();
        CreateMap<Country, CountryDto>().ReverseMap();
        CreateMap<Author, AuthorDto>().ReverseMap();
        CreateMap<Review, ReviewDto>().ReverseMap();
        CreateMap<Reviewer, ReviewerDto>().ReverseMap();
    }
}