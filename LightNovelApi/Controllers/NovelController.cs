using AutoMapper;
using LightNovelApi.Dto;
using LightNovelApi.Interfaces;
using LightNovelApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LightNovelApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NovelController : Controller
{
    private readonly INovelRepository _novelRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;
    public NovelController(INovelRepository novelRepository, IReviewRepository reviewRepository,  IMapper mapper)
    {
        _novelRepository = novelRepository;
        _reviewRepository = reviewRepository;
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
    
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult AddNovel([FromQuery] int authorId, [FromQuery] int genreId, [FromBody] NovelDto novelCreate)
    {
        if (novelCreate == null)
            return BadRequest(ModelState);

        var novels = _novelRepository
            .GetNovels()
            .FirstOrDefault(n => n.Title.Trim().ToUpper() == novelCreate.Title.TrimEnd().ToUpper());

        if (novels != null)
        {
            ModelState.AddModelError("", "Novel already exists!");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var novelMap = _mapper.Map<Novel>(novelCreate);

        if (_novelRepository.AddNovel(authorId, genreId, novelMap))
            return Ok("Successfully added novel!");

        ModelState.AddModelError("", "Something went wrong saving the novel");
        return StatusCode(500, ModelState);
    }
    
    [HttpPut("{novelId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult UpdateNovel(int novelId, [FromQuery] int authorId, [FromQuery] int genreId, [FromBody] NovelDto updatedNovel)
    {
        if (updatedNovel == null)
            return BadRequest(ModelState);

        if (novelId != updatedNovel.Id)
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var novelMap = _mapper.Map<Novel>(updatedNovel);

        if (_novelRepository.UpdateNovel(authorId, genreId, novelMap))
            return Ok("Successfully updated novel!");

        ModelState.AddModelError("", "Something went wrong updating the novel");
        return StatusCode(500, ModelState);
    }
    
    [HttpDelete("{novelId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult DeleteNovel(int novelId)
    {
        if (!_novelRepository.NovelExists(novelId))
            return NotFound();

        var reviews = _reviewRepository.GetReviewsOfANovel(novelId);
        var novel = _novelRepository.GetNovel(novelId);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if(!_reviewRepository.DeleteReviews(reviews.ToList()))
            return StatusCode(500, ModelState);

        if (_novelRepository.DeleteNovel(novel))
            return Ok("Successfully deleted novel!");

        ModelState.AddModelError("", "Something went wrong deleting the novel");
        return StatusCode(500, ModelState);
    }
}