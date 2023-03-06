using LightNovelApi.Data;
using LightNovelApi.Interfaces;
using LightNovelApi.Models;

namespace LightNovelApi.Repository;

public class CountryRepository : ICountryRepository
{
    private readonly DataContext _context;
    public CountryRepository(DataContext context)
    {
        _context = context;
    }
    public ICollection<Country> GetCountries()
    {
        return _context.Countries.ToList();
    }

    public Country GetCountry(int id)
    {
        return _context.Countries.Where(c => c.Id == id).FirstOrDefault();
    }

    public Country GetCountryByAuthor(int authorId)
    {
        return _context.Authors.Where(a => a.Id == authorId).Select(c => c.Country).FirstOrDefault();
    }

    public ICollection<Author> GetAuthorsByCountry(int countryId)
    {
        return _context.Authors.Where(c => c.Id == countryId).ToList();
    }

    public bool CountryExists(int id)
    {
        return _context.Countries.Any(c => c.Id == id);
    }
    public bool AddCountry(Country country)
    {
        _context.Add(country);
        return Save();
    }
    public bool UpdateCountry(Country country)
    {
        _context.Update(country);
        return Save();
    }
    public bool Save()
    {
        return _context.SaveChanges() > 0 ? true : false;
    }
}