using AutoMapper;
using LightNovelDb.Dto;
using LightNovelDb.Models;

namespace LightNovelDb.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Novel, NovelDto>().ReverseMap();
        CreateMap<Genre, GenreDto>().ReverseMap();
        CreateMap<Country, CountryDto>().ReverseMap();
        CreateMap<Author, AuthorDto>().ReverseMap();
        CreateMap<Review, ReviewDto>();
        CreateMap<Reviewer, ReviewerDto>();
    }
}