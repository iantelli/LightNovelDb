using LightNovelDb.Models;

namespace LightNovelDb.Interfaces;

public interface ICountryRepository
{
    ICollection<Country> GetCountries();
    Country GetCountry(int id);
    Country GetCountryByAuthor(int authorId);
    ICollection<Author> GetAuthorsByCountry(int countryId);
    bool CountryExists(int id);
}