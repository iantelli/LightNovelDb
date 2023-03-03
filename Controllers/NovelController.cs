using AutoMapper;
using LightNovelDb.Dto;
using LightNovelDb.Interfaces;
using LightNovelDb.Models;
using Microsoft.AspNetCore.Mvc;

namespace LightNovelDb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NovelController : Controller
{
    private readonly INovelRepository _novelRepository;
    private readonly IMapper _mapper;
    public NovelController(INovelRepository novelRepository, IMapper mapper)
    {
        _novelRepository = novelRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Novel>))]
    public IActionResult GetNovels()
    {
        var novels = _mapper.Map<List<NovelDto>>(_novelRepository.GetNovels());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(novels);
    }

    [HttpGet("{novelId}")]
    [ProducesResponseType(200, Type = typeof(Novel))]
    [ProducesResponseType(400)]
    public IActionResult GetNovel(int novelId)
    {
        if (!_novelRepository.NovelExists(novelId))
            return NotFound();

        var novel = _mapper.Map<NovelDto>(_novelRepository.GetNovel(novelId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(novel);
    }

    [HttpGet("{novelId}/rating")]
    [ProducesResponseType(200, Type = typeof(decimal))]
    [ProducesResponseType(400)]
    public IActionResult GetPokemonRating(int novelId)
    {
        if (!_novelRepository.NovelExists(novelId))
            return NotFound();

        var rating = _novelRepository.GetNovelRating(novelId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(rating);
    }
    
}