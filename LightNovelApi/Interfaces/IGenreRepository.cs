using LightNovelApi.Models;

namespace LightNovelApi.Interfaces;

public interface IGenreRepository
{
    ICollection<Genre> GetGenres();
    Genre GetGenre(int id);
    ICollection<Novel> GetNovelsByGenre(int genreId);
    bool GenreExists(int id);
    bool CreateGenre(Genre genre);
    bool Save();
}