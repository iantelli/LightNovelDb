using AutoMapper;
using LightNovelDb.Dto;
using LightNovelDb.Models;

namespace LightNovelDb.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Novel, NovelDto>();
    }
}