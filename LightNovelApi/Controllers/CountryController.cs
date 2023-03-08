using AutoMapper;
using LightNovelApi.Dto;
using LightNovelApi.Interfaces;
using LightNovelApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LightNovelApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : Controller
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;
    public CountryController(ICountryRepository countryRepository, IMapper mapper)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
    public IActionResult GetCountries()
    {
        var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(countries);
    }
    
    [HttpGet("{countryId}")]
    [ProducesResponseType(200, Type = typeof(Country))]
    [ProducesResponseType(400)]
    public IActionResult GetCountry(int countryId)
    {
        if (!_countryRepository.CountryExists(countryId))
            return NotFound();

        var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(country);
    }
    
    [HttpGet("author/{authorId}")]
    [ProducesResponseType(200, Type = typeof(Country))]
    [ProducesResponseType(400)]
    public IActionResult GetCountryByAuthor(int authorId)
    {
        if (!_countryRepository.CountryExists(authorId))
            return NotFound();

        var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryByAuthor(authorId));

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(country);
    }
    
    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateCountry([FromBody] CountryDto countryCreate)
    {
        if (countryCreate == null)
            return BadRequest(ModelState);

        var country = _countryRepository
            .GetCountries()
            .FirstOrDefault(g => g.Name.Trim().ToUpper() == countryCreate.Name.TrimEnd().ToUpper());

        if (country != null)
        {
            ModelState.AddModelError("", "Country already exists!");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var countryMap = _mapper.Map<Country>(countryCreate);

        if (!_countryRepository.AddCountry(countryMap))
        {
            ModelState.AddModelError("", "Something went wrong saving the country");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully added country!");
    }
    
    [HttpPut("{countryId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult UpdateCountry(int countryId, [FromBody] CountryDto countryUpdate)
    {
        if (countryUpdate == null)
            return BadRequest(ModelState);

        if (!_countryRepository.CountryExists(countryId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var countryMap = _mapper.Map<Country>(countryUpdate);

        if (!_countryRepository.UpdateCountry(countryMap))
        {
            ModelState.AddModelError("", "Something went wrong updating the country");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}