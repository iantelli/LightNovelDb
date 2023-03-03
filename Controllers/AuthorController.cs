using AutoMapper;
using LightNovelDb.Dto;
using LightNovelDb.Interfaces;
using LightNovelDb.Models;
using Microsoft.AspNetCore.Mvc;

namespace LightNovelDb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : Controller
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    public AuthorController(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
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
}