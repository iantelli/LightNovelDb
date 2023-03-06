using AutoMapper;
using LightNovelApi.Dto;
using LightNovelApi.Interfaces;
using LightNovelApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LightNovelApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : Controller
{
    private readonly IAuthorRepository _authorRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;
    public AuthorController(IAuthorRepository authorRepository, ICountryRepository countryRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _countryRepository = countryRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Author>))]
    public IActionResult GetAuthors()
    {
        var authors = _mapper.Map<List<AuthorDto>>(_authorRepository.GetAuthors());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(authors);
    }
    
    [HttpGet("{authorId}")]
    [ProducesResponseType(200, Type = typeof(Author))]
    [ProducesResponseType(400)]
    public IActionResult GetAuthor(int authorId)
    {
        if (!_authorRepository.AuthorExists(authorId))
            return NotFound();

        var author = _mapper.Map<AuthorDto>(_authorRepository.GetAuthor(authorId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(author);
    }

    [HttpGet("{authorId}/novel")]
    [ProducesResponseType(200, Type = typeof(Author))]
    [ProducesResponseType(400)]
    public IActionResult GetNovelsByAuthor(int authorId)
    {
        if(!_authorRepository.AuthorExists(authorId))
            return NotFound();
        
        var author = _mapper.Map<List<NovelDto>>(_authorRepository.GetNovelsByAuthor(authorId));
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(author);
    }
    
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateAuthor([FromQuery] int countryId, [FromBody] AuthorDto authorCreate)
    {
        if (authorCreate == null)
            return BadRequest(ModelState);

        var author = _authorRepository
            .GetAuthors()
            .FirstOrDefault(g => g.LastName.Trim().ToUpper() == authorCreate.LastName.TrimEnd().ToUpper());

        if (author != null)
        {
            ModelState.AddModelError("", "Author already exists!");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var authorMap = _mapper.Map<Author>(authorCreate);

        authorMap.Country = _countryRepository.GetCountry(countryId);

        if (!_authorRepository.AddAuthor(authorMap))
        {
            ModelState.AddModelError("", "Something went wrong saving the author");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully added author!");
    }
    
    [HttpPut("{authorId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult UpdateAuthor(int authorId, [FromBody] AuthorDto updatedAuthor)
    {
        if (updatedAuthor == null)
            return BadRequest(ModelState);

        if (!_authorRepository.AuthorExists(authorId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var authorMap = _mapper.Map<Author>(updatedAuthor);

        if (!_authorRepository.UpdateAuthor(authorMap))
        {
            ModelState.AddModelError("", "Something went wrong updating the author");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}