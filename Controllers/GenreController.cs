using AutoMapper;
using LightNovelDb.Dto;
using LightNovelDb.Interfaces;
using LightNovelDb.Models;
using Microsoft.AspNetCore.Mvc;

namespace LightNovelDb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController : Controller
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;
    public GenreController(IGenreRepository genreRepository, IMapper mapper)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
    public IActionResult GetGenres()
    {
        var genres = _mapper.Map<List<GenreDto>>(_genreRepository.GetGenres());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(genres);
    }
    
    [HttpGet("{genreId}")]
    [ProducesResponseType(200, Type = typeof(Genre))]
    [ProducesResponseType(400)]
    public IActionResult GetGenre(int genreId)
    {
        if (!_genreRepository.GenreExists(genreId))
            return NotFound();

        var genre = _mapper.Map<NovelDto>(_genreRepository.GetGenre(genreId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(genre);
    }

    [HttpGet("novel/{genreId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
    [ProducesResponseType(400)]
    public IActionResult GetNovelsByGenreId(int genreId)
    {
        var novels = _mapper.Map<List<NovelDto>>(_genreRepository.GetNovelsByGenre(genreId));
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(novels);
    }
    
}