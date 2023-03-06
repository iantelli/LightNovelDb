using LightNovelApi.Models;

namespace LightNovelApi.Interfaces;

public interface ICountryRepository
{
    ICollection<Country> GetCountries();
    Country GetCountry(int id);
    Country GetCountryByAuthor(int authorId);
    ICollection<Author> GetAuthorsByCountry(int countryId);
    bool CountryExists(int id);
    bool AddCountry(Country country);
    bool UpdateCountry(Country country);
    bool Save();
}