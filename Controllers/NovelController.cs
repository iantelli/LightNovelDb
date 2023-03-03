using LightNovelDb.Interfaces;
using LightNovelDb.Models;
using Microsoft.AspNetCore.Mvc;

namespace LightNovelDb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NovelController : Controller
{
    private readonly INovelRepository _novelRepository;
    public NovelController(INovelRepository novelRepository)
    {
        _novelRepository = novelRepository;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Novel>))]
    public IActionResult GetNovels()
    {
        var novels = _novelRepository.GetNovels();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(novels);
    }
}