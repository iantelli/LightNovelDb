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

        var genre = _mapper.Map<GenreDto>(_genreRepository.GetGenre(genreId));

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

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateGenre([FromBody] GenreDto genreCreate)
    {
        if (genreCreate == null)
            return BadRequest(ModelState);

        var genre = _genreRepository
            .GetGenres()
            .FirstOrDefault(g => g.Name.Trim().ToUpper() == genreCreate.Name.TrimEnd().ToUpper());

        if (genre != null)
        {
            ModelState.AddModelError("", "Genre already exists!");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var genreMap = _mapper.Map<Genre>(genreCreate);

        if (!_genreRepository.CreateGenre(genreMap))
        {
            ModelState.AddModelError("", "Something went wrong saving the genre");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created genre!");
    }
}