using AutoMapper;
using LightNovelApi.Dto;
using LightNovelApi.Interfaces;
using LightNovelApi.Models;
using LightNovelApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LightNovelApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : Controller
{
    private readonly IReviewRepository _reviewRepository;
    private readonly INovelRepository _novelRepository;
    private readonly IReviewerRepository _reviewerRepository;
    private readonly IMapper _mapper;

    public ReviewController(IReviewRepository reviewRepository, INovelRepository novelRepository, IReviewerRepository reviewerRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _novelRepository = novelRepository;
        _reviewerRepository = reviewerRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
    public IActionResult GetReviews()
    {
        var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(reviews);
    }
    
    [HttpGet("{reviewId:int}")]
    [ProducesResponseType(200, Type = typeof(Review))]
    [ProducesResponseType(400)]
    public IActionResult GetReview(int reviewId)
    {
        if (!_reviewRepository.ReviewExists(reviewId))
            return NotFound();

        var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(review);
    }
    
    [HttpGet("novel/{novelId:int}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
    [ProducesResponseType(400)]
    public IActionResult GetReviewsForANovel(int novelId)
    {
        if (!_reviewRepository.ReviewExists(novelId))
            return NotFound();

        var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfANovel(novelId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(reviews);
    }
    
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int novelId, [FromBody] ReviewDto reviewCreate)
    {
        if (reviewCreate == null)
            return BadRequest(ModelState);

        var reviews = _reviewRepository
            .GetReviews()
            .FirstOrDefault(r => r.Title.Trim().ToUpper() == reviewCreate.Title.TrimEnd().ToUpper());

        if (reviews != null)
        {
            ModelState.AddModelError("", "Review already exists!");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var reviewMap = _mapper.Map<Review>(reviewCreate);

        reviewMap.Novel = _novelRepository.GetNovel(novelId);
        reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);
        

        if (!_reviewRepository.CreateReview(reviewMap))
        {
            ModelState.AddModelError("", "Something went wrong saving the author");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully added review!");
    }
}