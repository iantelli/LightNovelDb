﻿using AutoMapper;
using LightNovelDb.Dto;
using LightNovelDb.Models;

namespace LightNovelDb.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Novel, NovelDto>();
        CreateMap<Genre, GenreDto>();
        CreateMap<Country, CountryDto>();
        CreateMap<Author, AuthorDto>();
        CreateMap<Review, ReviewDto>();
        CreateMap<Reviewer, ReviewerDto>();
    }
}